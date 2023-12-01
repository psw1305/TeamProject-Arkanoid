using UnityEngine;

public class ArkanoidGame
{
    public PaddleFire Paddle { get; set; }

    public void InstanceBall()
    {
        var ballStartPos = new Vector2(Paddle.transform.position.x, Paddle.transform.position.y + 0.3f);
        Managers.Resource.Instantiate("BallPrefab", ballStartPos);
    }
}
