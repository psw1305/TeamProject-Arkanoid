
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
        // 남아 있는 볼을 가져온다.
        var remainBall = Managers.Game.CurrentBalls.Find(ball => ball != null).GetComponent<Ball>();

        if (remainBall.BallState != BallPreference.BALL_STATE.READY) return;

        Managers.Event.PublishBallLaunch();
    }

    private void OnDisable()
    {
        _paddleController.OnFireEvent -= OnBallFire;
    }
}