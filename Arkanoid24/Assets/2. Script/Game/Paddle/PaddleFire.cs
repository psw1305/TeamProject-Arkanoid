using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PaddleFire : MonoBehaviour
{
    private bool _isBallLaunch = false;
    private bool _isBallCatch = true;

    private void Start()
    {
        Managers.Event.OnFireEvent += OnBallFire;
        Managers.Event.OnBallIsLaunch += OnBallIsLaunch;
    }

    private void OnBallFire()
    {
        var remainBall = Managers.Game.CurrentBalls.Find(ball => ball != null).GetComponent<Ball>();

        if (remainBall.BallState != BallPreference.BALL_STATE.READY) return;

        Managers.Event.PublishBallLaunch();
    }

    private void OnBallIsLaunch(bool isLaunch)
    {
        _isBallLaunch = isLaunch;
        _isBallCatch = !isLaunch;
    }
}