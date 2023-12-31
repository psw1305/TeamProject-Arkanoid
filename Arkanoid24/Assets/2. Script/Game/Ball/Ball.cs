using System;
using UnityEngine;

public class Ball : BallPreference
{
    #region Member Variables

    public bool isCatchLaunch = false;

    public int _defaultPower = 1;
    public int _maxPower = 1;

    public float _posX;

    // Event
    private event Action OnBallLaunch;

    #endregion


    #region Unity Flow
    protected override void Awake()
    {
        // Get Component
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        
        OnBallLaunch += BallToStart;

        SetAdditionalCurrentSpeed(BallOwner.GetComponent<BallSkillState>().BallExtraSpeed);
        SetPower(BallSkill.BallExtraPower);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        BallStateUpdateMethod();
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

    private void BallToStart()
    {
        if (BallState != BALL_STATE.READY) return;
        if(BallSkill.CurrentSkill == Items.Catch) isCatchLaunch = true;
        BallState = BALL_STATE.LAUNCH;

        CalculateBallPosToPaddle();
    }

    private void CalculateBallPosToPaddle()
    {
        var paddleWidth = _playerObject.GetComponent<BoxCollider2D>().bounds.size.x;
        var posX = _ballRbody.position.x - _paddleRbody.position.x;

        posX = posX / paddleWidth;
        
        var direction = new Vector2(posX, 1).normalized;

        if (posX != 0)
            _ballRbody.velocity = direction * _currentSpeed;
        else
            _ballRbody.velocity = Vector2.up * defaultSpeed;
        isCatchLaunch = false;
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
    #endregion


    #region Item SKill

    public void CheckCatchActivation()
    {
        if (BallSkill.CurrentSkill == Items.Catch)
        {
            BallState = BALL_STATE.READY;
            _posX = transform.position.x - _paddleRbody.transform.position.x;
        }
    }

    
    public void SetPower(int extraPower)
    {
        _maxPower = _defaultPower + extraPower;
        if(BallSkill.CurrentSkill == Items.Power)
        {
            transform.localScale = transform.localScale * 2f;
        }
    }

    #endregion


    public void CallBallLaunch()
    {
        OnBallLaunch?.Invoke();
    }

    private void OnDestroy()
    {
        OnBallLaunch -= BallToStart;
    }
}