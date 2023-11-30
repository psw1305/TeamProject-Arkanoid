
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleInputController : PaddleEventController
{
    #region Member Variables

    private Camera _mainCamera;

    #endregion


    #region Unity Flow
    private void Start()
    {
        // Data Caching
        _mainCamera = Camera.main;
    }
    #endregion


    #region Event Methods

    private void OnMovement(InputValue inputValue)
    {
        Vector2 mousePos = inputValue.Get<Vector2>();

        // 마우스 위치를 제한
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        // 월드 좌표로 변환
        mousePos = _mainCamera.ScreenToWorldPoint(mousePos);

        CallMovementEvent(mousePos);
    }

    private void OnFire(InputValue inputValue)
    {
        
    }

    #endregion
}