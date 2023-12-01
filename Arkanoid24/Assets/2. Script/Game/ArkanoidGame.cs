using UnityEngine;

public class ArkanoidGame : SingletonBehaviour<ArkanoidGame>
{
    private EdgeCollider2D screenEdge;

    [SerializeField] private GameObject paddle;
    [SerializeField] private GameObject ballPrefab;

    public PaddleFire GetPaddleFire()
    {
        return paddle.GetComponent<PaddleFire>();
    }

    protected override void Awake()
    {
        base.Awake();
        screenEdge = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        screenEdge.GenerateCameraBounds();
        FireBall();
    }

    public void FireBall()
    {
        var ballStartPos = new Vector2 (paddle.transform.position.x, paddle.transform.position.x - 3.5f);
        var ballClone = Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
        //ballClone.GetComponent<ArkanoidBall>().StartBall();
    }
}
