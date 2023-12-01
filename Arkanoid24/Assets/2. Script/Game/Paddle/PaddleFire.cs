using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFire : MonoBehaviour
{
    private bool _isBallLaunch = false;

    private void Start()
    {
        Managers.Event.OnFireEvent += OnBallFire;
        Managers.Event.OnBallIsLaunch += OnBallIsLaunch;
    }

    private void OnBallFire()
    {
        if (_isBallLaunch) return;

        Managers.Event.PublishBallLaunch();
    }

    private void OnBallIsLaunch(bool isLaunch)
    {
        _isBallLaunch = isLaunch;
    }
}