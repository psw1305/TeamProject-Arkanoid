using UnityEngine;

public class ArkanoidBall : MonoBehaviour
{
    [SerializeField] private GameManager game;

    [Header("Speed")]
    [SerializeField] public float ballMaxSpeed;
    [SerializeField] private float ballDefaultMaxSpeed;
    private float _defaultSpeed = 8f;

    private Rigidbody2D paddleBody;
    private Rigidbody2D ballBody;

    private bool isLaunch = false;
    private bool isCatchLaunch = false;
    private bool isCatch = false;

    private int _defaultPower = 1;
    private int _maxPower = 1;


    private float _posX;
    private float _paddleWidth;


    /// <summary>
    /// Ball 속도 설정 [임시]
    /// </summary>
    /// <param name="speed"></param>
    public void SetMaxSpeed(float speed)
    {
        ballMaxSpeed = ballDefaultMaxSpeed + speed;
    }

    private void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        paddleBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 인스턴스 관리 수정
        Managers.Event.OnBallLaunch += StartBall;
        SetMaxSpeed(Managers.Skill.BallExtraSpeed);
    }

    private void FixedUpdate()
    {
        if (Managers.Game.State == GameState.Pause)
        {
            ballBody.velocity = Vector3.zero;
            return;
        }

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

    #region Ball State

    public void StartBall()
    {
        if(isCatch) isCatchLaunch = true;
        isLaunch = true;
        isCatch = false;
        ballBody.velocity = new Vector2(0, 10);
        Managers.Event.PublishBallIsLaunch(isLaunch);
    }

    private void ReadyBall()
    {
        if(isCatch) FollowThePaddle(_posX);
        else FollowThePaddle();
    }

    private void FollowThePaddle(float posX = 0f)
    {
        Vector2 paddlePos = paddleBody.position;
        Vector2 newBallPos = new Vector2(paddlePos.x, paddlePos.y + 0.3f);
        ballBody.position = newBallPos + new Vector2(posX, 0f);
        ballBody.velocity = Vector2.zero;
    }

    private void LaunchBall()
    {
        if (ballBody.velocity.magnitude > ballMaxSpeed)
        {
            ballBody.velocity = ballBody.velocity.normalized * ballMaxSpeed;
        }
    }

    private void CatchLaunchBall()
    {
        var x = _posX / _paddleWidth;
        var dir = new Vector2(x, 1).normalized;
        ballBody.velocity = dir * ballMaxSpeed;

        isCatchLaunch = false;
    }

    #endregion

    #region Ball Collision

    /// <summary>
    /// 공과 패들의 x좌표에 따라 외각 백터 크기 부여
    /// </summary>
    /// <param name="paddlePos">패들 포지션</param>
    /// <param name="paddleWidth">패들 콜라이더 너비</param>
    /// <returns>서로 X위치 뺀 만큼 백터 x값 부여</returns>
    private float HitFactor(Vector2 paddlePos, float paddleWidth)
    {
        return (transform.position.x - paddlePos.x) / paddleWidth * 2f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _paddleWidth = col.collider.bounds.size.x;
            CheckCatchActivation();
            if (!isCatch)
            {
                var x = HitFactor(col.transform.position, _paddleWidth);
                var dir = new Vector2(x, 1).normalized;
                ballBody.velocity = dir * ballMaxSpeed;
            }
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
            _posX = transform.position.x - paddleBody.transform.position.x;
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
