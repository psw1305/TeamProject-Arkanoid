using UnityEngine;

public class ArkanoidGame
{
    public void InstanceBall()
    {
        var paddle = GameObject.FindWithTag("Player");
        var ballStartPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.3f);
        Managers.Resource.Instantiate("BallPrefab", ballStartPos);
    }
}
