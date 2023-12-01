using UnityEngine;

public class ArkanoidBall : MonoBehaviour
{
    [SerializeField] private ArkanoidGame game;

    [Header("Speed")]
    [SerializeField] public float ballMaxSpeed;

    private Rigidbody2D ballBody;
    private bool isLaunch = false;
    private PaddleFire paddleFire;

    /// <summary>
    /// Ball 속도 설정 [임시]
    /// </summary>
    /// <param name="speed"></param>
    public void SetMaxSpeed(float speed)
    {
        ballMaxSpeed = speed;
    }

    private void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 인스턴스 관리 수정
        paddleFire = Managers.Game.Paddle;
        paddleFire.OnBallFireRequest += StartBall;
    }

    private void FixedUpdate()
    {
        if (!isLaunch)
        {
            ReadyBall();
        }
        else
        {
            LaunchBall();
        }
    }

    #region Ball State

    public void StartBall()
    {
        isLaunch = true;
        ballBody.velocity = new Vector2(0, 10);
    }

    private void ReadyBall()
    {
        transform.position = paddleFire.transform.position;
    }

    private void LaunchBall()
    {
        if (ballBody.velocity.magnitude > ballMaxSpeed)
        {
            ballBody.velocity = ballBody.velocity.normalized * ballMaxSpeed;
        }
    }

    #endregion

    #region Ball Collision

    /// <summary>
    /// 공과 패들의 x좌표에 따라 외각 백터 크기 부여
    /// </summary>
    /// <param name="paddlePos">패들 포지션</param>
    /// <param name="paddleWidth">패들 콜라이더 너비</param>
    /// <returns>서로 X위치 뺀 만큼 백터 x값 부여</returns>
    private float HitFactor(Vector2 paddlePos, float paddleWidth)
    {
        return (transform.position.x - paddlePos.x) / paddleWidth * 2f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var x = HitFactor(col.transform.position, col.collider.bounds.size.x);
            var dir = new Vector2(x, 1).normalized;
            ballBody.velocity = dir * ballMaxSpeed;
        }
    }

    #endregion

    private void OnDestroy()
    {
        paddleFire.OnBallFireRequest -= StartBall;
    }
}
