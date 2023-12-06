
using UnityEngine;
using static BallPreference;

public class BallCollision : MonoBehaviour
{
    private Ball _ball;
    private Rigidbody2D _ballRbody;

    private void Awake()
    {
        _ballRbody = GetComponent<Rigidbody2D>();
        _ball = GetComponent<Ball>();
    }


    #region Collision

    private void OnCollisionEnter2D(Collision2D col)
    {
        var activePlayer = Managers.Player.GetActivePlayers();

        foreach (var player in activePlayer)
        {
            if (col.gameObject == player && _ball.BallState != BALL_STATE.READY)
            {
                SFX.Instance.PlayOneShot(SFX.Instance.paddleHit);
                float paddleWidth = col.collider.bounds.size.x;

                _ball.CheckCatchActivation();
                if (Managers.Skill.CurrentSkill != Items.Catch)
                {
                    var posX = HitFactor(col.transform.position, paddleWidth);
                    var direction = new Vector2(posX, 1).normalized;

                    _ballRbody.velocity = _ballRbody.velocity.magnitude * direction;
                }
            }
            else if (col.gameObject.CompareTag("Brick"))
            {
                _ball.BallHitCounting();
            }
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