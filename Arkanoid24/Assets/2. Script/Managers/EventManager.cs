
using System;
using UnityEngine;

public class EventManager
{
    #region Event Actions

    /* Paddle */
    public event Action<Vector2> OnMoveEvent;
    public event Action OnFireEvent;

    /* Ball */
    public event Action OnBallLaunch;

    #endregion


    #region Publish Paddle
    public void PublishMoveEvent(Vector2 axis)
    {
        OnMoveEvent?.Invoke(axis);
    }

    public void PublishFireEvent()
    {
        OnFireEvent?.Invoke();
    }
    #endregion


    #region Publish Ball
    public void PublishBallLaunch()
    {
        OnBallLaunch?.Invoke();
    }
    #endregion
}