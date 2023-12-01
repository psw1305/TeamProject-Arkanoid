using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFire : MonoBehaviour
{
    private PaddleEventController _controller;

    public event Action OnBallFireRequest;

    private void Awake()
    {
        Managers.Game.Paddle = this;
    }

    private void Start()
    {
        _controller = GetComponent<PaddleEventController>();
        _controller.OnLeftPressEvent += OnFire;
    }

    private void OnFire()
    {
        OnBallFireRequest?.Invoke();
    }
}