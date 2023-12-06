
using System;
using UnityEngine;

public class EventManager
{
    #region Event Actions

    /* Ball */
    public event Action OnBallLaunch;

    #endregion


    #region Publish Ball
    public void PublishBallLaunch()
    {
        OnBallLaunch?.Invoke();
    }
    #endregion
}