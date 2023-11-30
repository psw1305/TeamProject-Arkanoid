using UnityEngine;

public class ArkanoidBall : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float ballStartSpeed;
    [SerializeField] private float ballMaxSpeed;

    private Rigidbody2D ballBody;

    private void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveBall();
    }

    public void StartBall()
    {
        ballBody.velocity = new Vector2(0, ballStartSpeed);
    }

    private void MoveBall()
    {
        if (ballBody.velocity.magnitude > ballMaxSpeed)
        {
            ballBody.velocity = ballBody.velocity.normalized * ballMaxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
