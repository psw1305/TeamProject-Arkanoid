
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public enum PADDLE_STATE
    {
        READY,
        MOVEMENT
    }

    #region Member Variables

    // Input Field
    protected PaddleInputHandle _InputHandle;
    protected PlayerInput _playerInput;
    protected InputAction _movementAction;


    // Physics
    protected Rigidbody2D _rbody;
    protected Vector2 _direction;
    protected float _speed = 10f;

    // Camera
    [SerializeField] protected Camera _paddleCamera;

    #endregion



    #region Properties
    public PADDLE_STATE PaddleState {  get; set; } = PADDLE_STATE.READY;
    #endregion



    #region Unity Flow
    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _InputHandle = new PaddleInputHandle();

        // Service Register
        ServiceLocator.RegisterService(this);
    }

    private void FixedUpdate()
    {
        _direction = _movementAction.ReadValue<Vector2>().normalized;

        _rbody.velocity = new Vector2(_direction.x * _speed * Time.fixedDeltaTime, 0);
    }

    private void OnEnable()
    {
        _movementAction = _InputHandle.Paddle.Movement;
        _InputHandle?.Paddle.Enable();
    }

    private void OnDisable()
    {
        _InputHandle?.Paddle.Disable();
    }
    #endregion
}