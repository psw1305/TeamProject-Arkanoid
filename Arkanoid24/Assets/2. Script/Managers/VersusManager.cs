
using System.Collections.Generic;
using UnityEngine;

public class VersusManager
{
    #region Properties

    public VersusScene Versus { get; private set; }
    public VersusSceneUI VersusUI { get; private set; }
    public VersusLevelBlueprint[] VersusStages { get; private set; }
    public int CurrentLevel { get; set; } = 0;
    public int Player1Bricks { get; set; }
    public int Player2Bricks { get; set; }
    public int Life { get; set; }
    public List<int> PlayerLife {  get; set; }

    #endregion


    #region Init
    public void Initialize()
    {
        VersusStages = Managers.Resource.GetVersusStages();
    }

    public void InitScene()
    {
        Versus = Object.FindObjectOfType<VersusScene>();
        VersusUI = Object.FindObjectOfType<VersusSceneUI>();

        Managers.Game.State = GameState.Play;

        Player1Bricks = VersusStages[CurrentLevel].Player1Bricks;
        Player2Bricks = VersusStages[CurrentLevel].Player2Bricks;
    }
    #endregion

    public VersusLevelBlueprint CurrentStage()
    {
        return VersusStages[CurrentLevel];
    }
    

    public void Player1BrickCount()
    {
        Player1Bricks--;

        if (Player1Bricks == 0)
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