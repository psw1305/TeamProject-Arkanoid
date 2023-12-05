
using UnityEngine;
using static BallPreference;

public class VersusCollision : MonoBehaviour
{
    private VersusPlayBall _ball;
    private Rigidbody2D _ballRbody;

    private void Awake()
    {
        _ballRbody = GetComponent<Rigidbody2D>();
        _ball = GetComponent<VersusPlayBall>();
    }


    #region Collision

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Player1") &&
            _ball.BallState != VersusPlayBallPreference.BALL_STATE.READY) || (col.gameObject.CompareTag("Player2") &&
            _ball.BallState != VersusPlayBallPreference.BALL_STATE.READY))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.paddleHit);
            float paddleWidth = col.collider.bounds.size.x;

            _ball.CheckCatchActivation();
            if (!_ball.isCatch)
            {
                var posX = HitFactor(col.transform.position, paddleWidth);
                var direction = new Vector2(posX, 1).normalized;

                _ballRbody.velocity = _ballRbody.velocity.magnitude * direction;
            }
        }
        else
        {
            _ball.BallHitCounting();
        }
    }

    #endregion


    #region Utility
    private float HitFactor(Vector2 paddlePos, float paddleWidth)
    {
        return (_ballRbody.position.x - paddlePos.x) / paddleWidth * 2f;
    }
#endregion
}