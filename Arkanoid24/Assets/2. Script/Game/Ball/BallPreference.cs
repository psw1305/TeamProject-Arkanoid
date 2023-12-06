
using UnityEngine;

public class BallPreference : MonoBehaviour
{
    public enum BALL_STATE
    {
        READY,
        LAUNCH
    }

    #region Member Variables

    [Header("Ball Information")]
    [Range(4f, 6f)] public float MinSpeed = 4f;
    [Range(14f, 16f)] public float MaxSpeed = 16f;
    // Ball Start Default Speed
    [Range(7f, 12f)] public float defaultSpeed = 7f;

    [SerializeField] protected float _currentSpeed;

    protected Rigidbody2D _paddleRbody;
    protected Rigidbody2D _ballRbody;

    public int ballHitCount = 0;
    public float ballIncreaseSpeedScope = 0.5f;

    public BALL_STATE BallState { get; set; } = BALL_STATE.READY;

    #endregion


    #region Unity Flow
    protected virtual void Awake()
    {
        _paddleRbody = ServiceLocator.GetService<PaddleController>().GetComponent<Rigidbody2D>();
        _ballRbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        _currentSpeed = defaultSpeed;
    }

    protected virtual void FixedUpdate()
    {
        if(Managers.Game.State != GameState.Play)
        {
            _ballRbody.velocity = Vector2.zero;
            return;
        }

    }
    #endregion


    #region Utility
    public void SetAdditionalCurrentSpeed(float additionalSpeed)
    {
        _currentSpeed = defaultSpeed + additionalSpeed;

        if (_currentSpeed > MaxSpeed)
            _currentSpeed = MaxSpeed;
        else if(_currentSpeed < MinSpeed)
            _currentSpeed = MinSpeed;
    }

    public void BallHitCounting()
    {
        if(ballHitCount++ >= 3)
        {
            //SetAdditionalCurrentSpeed(ballIncreaseSpeedScope);
            Managers.Skill.BallIncreaseSpeed += ballIncreaseSpeedScope;
            Managers.Skill.SetSpeed();
            ballHitCount = 0;
        }
    }

    public bool InvalidCheckDirection(Vector2 direction)
    {
        return (direction == Vector2.zero) ? false : true;
    }
    #endregion
}