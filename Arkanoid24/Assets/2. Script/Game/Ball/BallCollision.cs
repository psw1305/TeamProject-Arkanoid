
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private Rigidbody2D _ballRbody;

    private void Start()
    {
        _ballRbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.CompareTag("Player"))
        //{
        //    SFX.Instance.PlayOneShot(SFX.Instance.paddleHit);
        //    _paddleWidth = col.collider.bounds.size.x;

        //    CheckCatchActivation();
        //    if (!isCatch)
        //    {
        //        var x = HitFactor(col.transform.position, _paddleWidth);
        //        var dir = new Vector2(x, 1).normalized;
        //        ballBody.velocity = dir * ballMaxSpeed;
        //    }
        //}
    }

    private float HitFactor(Vector2 paddlePos, float paddleWidth)
    {
        return (_ballRbody.position.x - paddlePos.x) / paddleWidth * 2f;
    }
}