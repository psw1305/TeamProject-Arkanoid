
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().name != "TEST_Versus")
        {
            var remainBall = Managers.Game.CurrentBalls.Find(ball => ball != null).GetComponent<Ball>();

            if (remainBall.BallState != BallPreference.BALL_STATE.READY) return;

            Managers.Event.PublishBallLaunch();
        }
        else
        {
            // 남아 있는 볼을 가져온다.
            var remainBall = Managers.Versus.PlayersBalls["Player1"].Find(ball => ball != null).GetComponent<VersusPlayBall>();
            //var remainBall = Managers.Versus.PlayersBalls["Player2"].Find(ball => ball != null).GetComponent<VersusPlayBall>();

            //if (remainBall.BallState != BallPreference.BALL_STATE.READY) return;

            Managers.Event.PublishBallLaunch();
        }
    }

    private void OnDisable()
    {
        _paddleController.OnFireEvent -= OnBallFire;
    }
}