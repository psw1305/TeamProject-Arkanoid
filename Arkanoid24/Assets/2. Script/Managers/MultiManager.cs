
using System.Collections.Generic;
using UnityEngine;

public class MultiManager
{
    public Dictionary<int, List<GameObject>> PlayerBalls = new();

    public GameState State { get; set; }
    public List<int> PlayerScore {  get; set; }
    public List<int> PlayerLife {  get; set; }

    public void Initalize()
    {

    }

    public void InstanceBall(int playerKey)
    {
        string playerTag = "Player" + playerKey.ToString();
        var paddle = GameObject.FindWithTag(playerTag);
        var ballStartPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.5f);
        //var ballClone = Managers.Resource.Instantiate("BallPrefab", ballStartPos);
        //PlayerBalls[] = 
    }
}
