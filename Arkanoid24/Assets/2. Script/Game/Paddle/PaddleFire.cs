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
        if (_isBallLaunch && _isBallCatch) return;
        Managers.Event.PublishBallLaunch();
    }
    private void OnBallIsLaunch(bool isLaunch)
    {
        _isBallLaunch = isLaunch;
        _isBallCatch = !isLaunch;
    }
}