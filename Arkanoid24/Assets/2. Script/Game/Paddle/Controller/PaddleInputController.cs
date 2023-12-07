
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleInputController : PaddleController
{

    #region Unity Flow
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    #endregion


    #region Event Methods

    private void OnMovement(InputValue inputValue)
    {
        if (Managers.Game.State != GameState.Play) return;

        Action<InputValue> movementAction = 
            (Managers.Game.Mode == GameMode.Versus) ? KeyboardMovementToMulti : MouseMovementToSolo;

        movementAction?.Invoke(inputValue);
    }

    private void OnFire(InputValue inputValue)
    {
        if (Managers.Game.State != GameState.Play) return;

        CallFireEvent();
    }

    #endregion


    #region Multi / Solo
    private void MouseMovementToSolo(InputValue inputValue)
    {
        Vector2 mousePos = inputValue.Get<Vector2>();

        // 마우스 위치를 제한
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        // 월드 좌표로 변환
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        CallMoveEvent(mousePos);
    }

    private void KeyboardMovementToMulti(InputValue inputValue)
    {
        Vector2 keyboardPos = inputValue.Get<Vector2>();

        CallMoveEvent(keyboardPos);
    }
    #endregion


    #region Utilities
    private void SettingCameraIsMulti()
    {

    }
    #endregion
}