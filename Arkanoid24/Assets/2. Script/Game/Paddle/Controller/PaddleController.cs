
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

    // Literals
    public const string SoloMaps = "SoloPaddle";
    public const string MultiMaps1 = "MultiPaddle1";
    public const string MultiMaps2 = "MultiPaddle2";

    // Input Field
    protected PlayerInput _playerInput;

    // Camera
    [SerializeField] protected Camera _paddleCamera;

    #endregion



    #region Properties
    public PADDLE_STATE PaddleState {  get; set; } = PADDLE_STATE.READY;
    #endregion



    #region Unity Flow
    protected virtual void Awake()
    {
        // Get Component
        _playerInput = GetComponent<PlayerInput>();

        // Service Register
        ServiceLocator.RegisterService(this);
    }

    protected virtual void Start()
    {
        PaddleMapsSetting();
    }
    #endregion



    #region Swtich Action Maps
    private void PaddleMapsSetting()
    {
        Action paddleMaps = (Managers.Game.IsMulti == true) ?
            EnableMultiPaddle : EnableSoloPaddle;

        paddleMaps?.Invoke();
    }

    public void EnableSoloPaddle()
    {
        _playerInput.SwitchCurrentActionMap(SoloMaps);
    }

    public void EnableMultiPaddle()
    {
        if (gameObject.CompareTag(PlayerManager.TagPlayer1))
            _playerInput.SwitchCurrentActionMap(MultiMaps1);
        else if (gameObject.CompareTag(PlayerManager.TagPlayer2))
            _playerInput.SwitchCurrentActionMap(MultiMaps2);
    }
    #endregion

}