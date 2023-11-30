using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFire : MonoBehaviour
{
    private PaddleEventController _controller;

    private GameObject _ballObject;
    private ArkanoidBall _ballControll;

    private void Start()
    {
        _controller = GetComponent<PaddleEventController>();
        _ballObject = GameObject.FindWithTag("Ball");
        _ballControll = _ballObject.GetComponent<ArkanoidBall>();

        _controller.OnLeftPressEvent += OnFire;
    }

    private void OnFire()
    {
        _ballControll.StartBall();
    }
}