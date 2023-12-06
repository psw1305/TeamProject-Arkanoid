
using System;
using UnityEngine;

public class PaddleFire : MonoBehaviour
{
    private PaddleController _paddleController;

    private void Start()
    {
        _paddleController = GetComponent<PaddleController>();

        // 이벤트 등록
        _paddleController.OnFireEvent += OnBallFire;
    }

    private void OnBallFire()
    {
        var balls = Managers.Ball.GetBallsForPlayer(gameObject);
        foreach (var ball in balls)
        {
            ball.GetComponent<Ball>().CallBallLaunch();
        }
    }

    private void OnDisable()
    {
        _paddleController.OnFireEvent -= OnBallFire;
    }
}