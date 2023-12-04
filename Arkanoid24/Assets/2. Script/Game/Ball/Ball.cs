using UnityEngine;

public class Ball : BallPreference
{
    [SerializeField] public float ballMaxSpeed;
    [SerializeField] private float ballDefaultMaxSpeed;

    private bool isLaunch = false;
    private bool isCatchLaunch = false;
    private bool isCatch = false;

    private int _defaultPower = 1;
    private int _maxPower = 1;


    private float _posX;
    private float _paddleWidth;


    public void SetMaxSpeed(float speed)
    {
        ballMaxSpeed = ballDefaultMaxSpeed + speed;
    }

    #region Unity Flow
    protected override void Start()
    {
        base.Start();

        Managers.Event.OnBallLaunch += StartBall;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isLaunch || isCatch)
        {
            ReadyBall();
        }
        else if (isCatchLaunch)
        {
            CatchLaunchBall();
        }
        else
        {
            LaunchBall();
        }
    }

    #endregion

    #region Ball State

    public void StartBall()
    {
        if(isCatch) isCatchLaunch = true;
        isLaunch = true;
        isCatch = false;
        _ballRbody.velocity = Vector2.up * defaultSpeed;
        Managers.Event.PublishBallIsLaunch(isLaunch);
    }

    private void ReadyBall()
    {
        if(isCatch) FollowThePaddle(_posX);
        else FollowThePaddle();
    }

    private void FollowThePaddle(float posX = 0f)
    {
        Vector2 paddlePos = _paddleRbody.position;
        Vector2 newBallPos = new Vector2(paddlePos.x, paddlePos.y + 0.3f);
        _ballRbody.position = newBallPos + new Vector2(posX, 0f);
        _ballRbody.velocity = Vector2.zero;
    }

    private void LaunchBall()
    {
        if(_ballRbody.velocity.magnitude > _currentSpeed)
        {
            _ballRbody.velocity = _currentDirection * _currentSpeed;
        }

        if (_ballRbody.velocity.magnitude > ballMaxSpeed)
        {
            _ballRbody.velocity = _ballRbody.velocity.normalized * ballMaxSpeed;
        }
    }

    private void CatchLaunchBall()
    {
        var x = _posX / _paddleWidth;
        var dir = new Vector2(x, 1).normalized;
        _ballRbody.velocity = dir * ballMaxSpeed;

        isCatchLaunch = false;
    }

    #endregion

    #region Ball Collision

    private float HitFactor(Vector2 paddlePos, float paddleWidth)
    {
        return (transform.position.x - paddlePos.x) / paddleWidth * 2f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.paddleHit);
            _paddleWidth = col.collider.bounds.size.x;

            CheckCatchActivation();
            if (!isCatch)
            {
                var x = HitFactor(col.transform.position, _paddleWidth);
                var dir = new Vector2(x, 1).normalized;
                _ballRbody.velocity = dir * ballMaxSpeed;
            }
        }
        else
        {
            SetAdditionalCurrentSpeed(1.5f);
        }
    }

    #endregion

    private void OnDestroy()
    {
        if (Managers.Instance != null && Managers.Event != null)
        {
            Managers.Event.PublishBallIsLaunch(false);
            Managers.Event.OnBallLaunch -= StartBall;
        }
    }
    #region Item SKill

    private void CheckCatchActivation()
    {
        if (Managers.Skill.CurrentSkill == Items.Catch)
        {
            isCatch = true;
            _posX = transform.position.x - _paddleRbody.transform.position.x;
        }
        else
        {
            isCatch = false;
        }

    }

    
    public void SetPower(int extraPower)
    {
        _maxPower = _defaultPower + extraPower;
    }

    #endregion
}
