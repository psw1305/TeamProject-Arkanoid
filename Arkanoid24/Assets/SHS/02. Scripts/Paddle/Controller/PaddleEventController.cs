using System;
using UnityEngine;

public class PaddleEventController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnLeftPressEvent;

    protected bool IsLeftPressed { get; private set; }

    #region Call Events
    public void CallMovementEvent(Vector2 axisX)
    {
        OnMoveEvent?.Invoke(axisX);
    }

    public void CallLeftButtonPressEvent()
    {
        OnLeftPressEvent?.Invoke();
    }
    #endregion
}