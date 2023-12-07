
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

    // Game Pause
    protected Vector2 _prevVelocity;
    protected bool _isStarted = true;

    public float ballIncreaseSpeedScope = 0.5f;

    // Player Depend, Reference
    protected GameObject _playerObject;

    public BALL_STATE BallState { get; set; } = BALL_STATE.READY;
    
    // Getter
    public GameObject BallOwner { get { return _playerObject; } }

    #endregion


    #region Unity Flow
    protected virtual void Awake()
    {
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
            if (_isStarted)
            {
                _isStarted = false;
                _prevVelocity = _ballRbody.velocity;
            }

            _ballRbody.velocity = Vector2.zero;
            return;
        }

        if (!_isStarted)
        {
            _ballRbody.velocity = _prevVelocity;
            _isStarted = true;
        }
    }
    #endregion


    #region Utility
    public void AssignPlayer(GameObject player)
    {
        _playerObject = player;

        GetPlayerComponent();
    }

    private void GetPlayerComponent()
    {
        _paddleRbody = _playerObject.GetComponent<Rigidbody2D>();
    }

    public void SetAdditionalCurrentSpeed(float additionalSpeed)
    {
        _currentSpeed = defaultSpeed + additionalSpeed;

        if (_currentSpeed > MaxSpeed)
            _currentSpeed = MaxSpeed;
        else if(_currentSpeed < MinSpeed)
            _currentSpeed = MinSpeed;
    }

    public void BallHitBrick()
    {
        Managers.Skill.SetSpeed(BallOwner);
    }

    public bool InvalidCheckDirection(Vector2 direction)
    {
        return (direction == Vector2.zero) ? false : true;
    }
    #endregion
}