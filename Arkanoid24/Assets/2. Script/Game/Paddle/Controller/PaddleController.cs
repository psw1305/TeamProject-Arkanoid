
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    #region STATE
    public enum PADDLE_STATE
    {
        READY,
        MOVEMENT
    }
    #endregion



    #region Member Variables

    public const string SoloMaps = "SoloPaddle";
    public const string MultiMaps = "MultiPaddle";

    // Input Field
    protected PlayerInput _playerInput;

    // Physics
    protected Rigidbody2D _rbody;
    protected float _paddleSpeed = 10f;

    // Camera
    [SerializeField] protected Camera _paddleCamera;

    #endregion



    #region Properties
    public PADDLE_STATE PaddleState {  get; set; } = PADDLE_STATE.READY;
    #endregion



    #region Unity Flow
    private void Awake()
    {
        // Get Component
        _playerInput = GetComponent<PlayerInput>();
        _rbody = GetComponent<Rigidbody2D>();

        // Service Register
        ServiceLocator.RegisterService(this);
    }

    private void OnEnable()
    {
        Action enableAction = (isMulti == true) ? EnableMultiPaddle : EnableSoloPaddle;

        
        EnableMultiPaddle();
    }
    #endregion


    #region Swtich Action Maps
    public void EnableSoloPaddle()
    {
        _playerInput.SwitchCurrentActionMap(SoloMaps);
    }

    public void EnableMultiPaddle()
    {
        _playerInput.SwitchCurrentActionMap(MultiMaps);
    }
    #endregion


    #region TES
    public bool isMulti = true;

    private void FixedUpdate()
    {
        if (isMulti)
        {
            ProcessMultiplayerInput();
        }
        else
        {
            //ProcessSingleplayerInput();
        }
    }

    private void ProcessMultiplayerInput()
    {
        // 멀티플레이어 모드에서 플레이어 1의 입력을 읽습니다.
        Vector2 player1Input = _playerInput.actions["Movement"].ReadValue<Vector2>();
        // 플레이어 1의 Rigidbody2D를 이동시킵니다.
        // 이 예에서는 x축 방향으로만 이동한다고 가정합니다.
        MovePaddle(_rbody, player1Input.x);

        // 멀티플레이어 모드에서 플레이어 2의 입력을 읽습니다.
        Vector2 player2Input = _playerInput.actions["Movement"].ReadValue<Vector2>();
        // 플레이어 2의 Rigidbody2D를 이동시킵니다.
        // 플레이어 2의 Rigidbody2D를 별도로 관리해야 할 수도 있습니다.
        // MovePaddle(player2Rigidbody, player2Input.x);

        Debug.Log("Multiplayer Input Player 1: " + player1Input);
        Debug.Log("Multiplayer Input Player 2: " + player2Input);
    }

    private void ProcessSingleplayerInput()
    {
        // 싱글 플레이어 모드에서 마우스의 위치를 읽습니다.
        Vector2 mouseInput = _playerInput.actions["MousePosition"].ReadValue<Vector2>();
        // 마우스 위치에 따라 Rigidbody2D를 이동시킵니다.
        MovePaddleWithMouse(mouseInput);

        Debug.Log("Singleplayer Mouse Input: " + mouseInput);
    }

    private void MovePaddle(Rigidbody2D rbody, float inputX)
    {
        // Rigidbody2D를 이동시키는 로직을 구현합니다.
        rbody.velocity = new Vector2(inputX * _paddleSpeed, rbody.velocity.y);
    }

    private void MovePaddleWithMouse(Vector2 mousePosition)
    {
        // 마우스 위치에 따라 Rigidbody2D를 이동시키는 로직을 구현합니다.
        // 마우스 포인터를 월드 좌표로 변환할 필요가 있습니다.
        Vector2 worldPosition = _paddleCamera.ScreenToWorldPoint(mousePosition);
        _rbody.position = new Vector2(worldPosition.x, _rbody.position.y);
    }
    #endregion
}