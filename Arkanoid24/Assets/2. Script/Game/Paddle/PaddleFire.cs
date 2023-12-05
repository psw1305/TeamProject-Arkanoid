
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleFire : MonoBehaviour
{
    private void Start()
    {
        // 좌클릭 이벤트 등록
        Managers.Event.OnFireEvent += OnBallFire;
    }

    private void OnBallFire()
    {
        if(SceneManager.GetActiveScene().name != "VersusMode")
        {
            var remainBall = Managers.Game.CurrentBalls.Find(ball => ball != null).GetComponent<Ball>();

            if (remainBall.BallState != BallPreference.BALL_STATE.READY) return;

            Managers.Event.PublishBallLaunch();
        }
        else
        {
            // 남아 있는 볼을 가져온다.
            var remainBall = Managers.Versus.PlayersBalls.Find(ball => ball != null).GetComponent<VersusPlayBall>();

            //if (remainBall.BallState != BallPreference.BALL_STATE.READY) return;

            Managers.Event.PublishBallLaunch();
        }
    }
}