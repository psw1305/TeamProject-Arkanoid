
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
    [Range(7f, 9f)] public float defaultSpeed = 8f;

    [SerializeField] protected float _currentSpeed;
    [SerializeField] protected Vector2 _currentDirection;

    protected Rigidbody2D _paddleRbody;
    protected Rigidbody2D _ballRbody;

    protected int _ballHitCount = 0;
    protected float _ballIncreaseSpeedScope = 1.5f;

    public BALL_STATE BallState { get; set; } = BALL_STATE.READY;

    #endregion


    #region Unity Flow
    protected virtual void Start()
    {
        _paddleRbody = ServiceLocator.GetService<PaddleController>().GetComponent<Rigidbody2D>();
        _ballRbody = GetComponent<Rigidbody2D>();

        _currentSpeed = defaultSpeed;
        _currentDirection = Vector2.up;
        // SetMaxSpeed(Managers.Skill.BallExtraSpeed);
    }

    protected virtual void FixedUpdate()
    {
        if(Managers.Game.State == GameState.Pause)
        {
            _ballRbody.velocity = Vector2.zero;
            return;
        }

        if(BallState != BALL_STATE.READY)
            SetBallCurrentPhsyicsInfo();
    }
    #endregion


    #region Utility
    public void SetAdditionalCurrentSpeed(float additionalSpeed)
    {
        _currentSpeed = _currentSpeed + additionalSpeed;
    }

    private void SetBallCurrentPhsyicsInfo()
    {
        _currentSpeed = _ballRbody.velocity.magnitude;
        _currentDirection = _ballRbody.velocity.normalized;
    }
    #endregion
}