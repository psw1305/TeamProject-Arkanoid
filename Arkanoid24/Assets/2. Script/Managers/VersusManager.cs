
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class VersusManager
{
    public List<GameObject> PlayersBalls = new();

    public VersusScene Versus { get; private set; }
    public VersusSceneUI VersusUI { get; private set; }
    public GameState State { get; set; }
    public VersusLevelBlueprint[] VersusStages { get; private set; }
    public int CurrentLevel = 0;
    public int Player1Bricks { get; set; }
    public int Player2Bricks { get; set; }
    public int Life { get; set; }
    public List<int> PlayerLife {  get; set; }

    public void Initialize()
    {
        PlayersBalls.Clear();

        Versus = Object.FindObjectOfType<VersusScene>();
        VersusUI = Object.FindObjectOfType<VersusSceneUI>();
        State = GameState.Play;
        VersusStages = Managers.Resource.GetVersusStages();
        Player1Bricks = VersusStages[CurrentLevel].Player1Bricks;
        Player2Bricks = VersusStages[CurrentLevel].Player2Bricks;
    }

    public VersusLevelBlueprint CurrentStage()
    {
        return VersusStages[CurrentLevel];
    }

    public void InstanceBall()
    {
        var player1Paddle = GameObject.FindWithTag("Player1");
        var Player1ballStartPos = new Vector2(player1Paddle.transform.position.x, player1Paddle.transform.position.y + 0.5f);
        var player1BallClone = Managers.Resource.Instantiate("BallPrefab(Player1)", Player1ballStartPos);
        PlayersBalls.Add(player1BallClone);
        var player2Paddle = GameObject.FindWithTag("Player2");
        var Player2ballStartPos = new Vector2(player2Paddle.transform.position.x, player2Paddle.transform.position.y + 0.5f);
        var player2BallClone = Managers.Resource.Instantiate("BallPrefab(Player2)", Player2ballStartPos);
        PlayersBalls.Add(player2BallClone);
    }
    public void Player1BrickCount()
    {
        Player1Bricks--;

        if(Player1Bricks == 0)
        {
            VersusUI.ShowGameOver("Player1");
        }
    }
    public void Player2BrickCount()
    {
        Player2Bricks--;

        if (Player2Bricks == 0)
        {
            VersusUI.ShowGameOver("Player2");
        }
    }
}
