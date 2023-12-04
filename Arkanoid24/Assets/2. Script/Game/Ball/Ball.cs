using System;
using UnityEngine;

public class Ball : BallPreference
{
    [SerializeField] public float ballMaxSpeed;
    [SerializeField] private float ballDefaultMaxSpeed;

    public bool isLaunch = false;
    public bool isCatchLaunch = false;
    public bool isCatch = false;

    public int _defaultPower = 1;
    public int _maxPower = 1;


    public float _posX;
    public float _paddleWidth;


    //public void SetMaxSpeed(float speed)
    //{
    //    ballMaxSpeed = ballDefaultMaxSpeed + speed;
    //}

    #region Unity Flow
    protected override void Awake()
    {
        // Get Component
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        Managers.Event.OnBallLaunch += BallToStart;

        SetAdditionalCurrentSpeed(Managers.Skill.BallExtraSpeed);
        //SetMaxSpeed(Managers.Skill.BallExtraSpeed);
        SetPower(Managers.Skill.BallExtraPower);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        BallStateUpdateMethod();
        //if (!isLaunch || isCatch)
        //{
        //    BallToReady();
        //}
        //else if (isCatchLaunch)
        //{
        //    CatchLaunchBall();
        //}
        //else
        //{
        //    BallToLaunch();
        //}
    }

    #endregion

    #region Ball State

    private void BallStateUpdateMethod()
    {
        Action ballAction = BallState switch
        {
            BALL_STATE.READY => () => BallToReady(),
            BALL_STATE.LAUNCH => () => BallToLaunch(),
            _ => () => { }
        };

        ballAction();
    }

    public void BallToStart()
    {
        if(isCatch) isCatchLaunch = true;
        isLaunch = true;
        isCatch = false;
        BallState = BALL_STATE.LAUNCH;

        _paddleWidth = ServiceLocator.GetService<PaddleController>().GetComponent<BoxCollider2D>().bounds.size.x;
        var posX = _posX / _paddleWidth;
        var dir = new Vector2(posX, 1).normalized;
        if (posX != 0)
            _ballRbody.velocity = dir * _currentSpeed;
        else
            _ballRbody.velocity = Vector2.up * defaultSpeed;
        Managers.Event.PublishBallIsLaunch(isLaunch);
    }

    private void BallToReady()
    {
        FollowThePaddle(_posX);
    }

    private void FollowThePaddle(float posX = 0f)
    {
        Vector2 paddlePos = _paddleRbody.position;
        Vector2 newBallPos = new Vector2(paddlePos.x, paddlePos.y + 0.5f);

        _ballRbody.position = newBallPos + new Vector2(posX, 0f);
        _ballRbody.velocity = Vector2.zero;
    }

    private void BallToLaunch()
    {
        var ballVelocitySpeed = _ballRbody.velocity.magnitude;

        if(ballVelocitySpeed > _currentSpeed || ballVelocitySpeed < _currentSpeed)
        {
            _ballRbody.velocity = _ballRbody.velocity.normalized * _currentSpeed;
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



    #region Item SKill

    public void CheckCatchActivation()
    {
        if (Managers.Skill.CurrentSkill == Items.Catch)
        {
            BallState = BALL_STATE.READY;
            _posX = transform.position.x - _paddleRbody.transform.position.x;
        }
    }

    
    public void SetPower(int extraPower)
    {
        _maxPower = _defaultPower + extraPower;
        if(Managers.Skill.CurrentSkill == Items.Power)
        {
            transform.localScale = transform.localScale * 2f;
        }
    }

    #endregion



    private void OnDestroy()
    {
        if (Managers.Instance != null && Managers.Event != null)
        {
            Managers.Event.PublishBallIsLaunch(false);
            Managers.Event.OnBallLaunch -= BallToStart;
        }
    }
}
