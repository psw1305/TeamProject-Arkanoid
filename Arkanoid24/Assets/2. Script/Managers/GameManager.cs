using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public List<GameObject> CurrentBalls = new();

    #region Properties

    public MainScene Main { get; private set; }
    public MainSceneUI MainUI { get; private set; }
    public GameState State { get; set; }
    public StageBlueprint[] Stages { get; private set; }
    public int CurrentLevel { get; set; }
    public int Bricks { get; set; }
    public float Score { get; set; }
    public float BestScore { get; set; }
    public int Life { get; set; }
    
    #endregion

    public void Initialize()
    {
        CurrentBalls.Clear();

        Main = Object.FindObjectOfType<MainScene>();
        MainUI = Object.FindObjectOfType<MainSceneUI>();
        State = GameState.Play;
        Stages = Managers.Resource.GetStages();
        Bricks = Stages[CurrentLevel].Bricks;
    }

    public StageBlueprint CurrentStage()
    {
        return Stages[CurrentLevel];
    }

    public void InstanceBall()
    {
        var paddle = GameObject.FindWithTag("Player");
        var ballStartPos = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.5f);
        var ballClone = Managers.Resource.Instantiate("BallPrefab", ballStartPos);
        CurrentBalls.Add(ballClone);
    }

    public void AddScore(float score)
    {
        Bricks--;
        if (MainUI == null) return;
        if (Bricks == 0)
        {
            State = GameState.Pause;
            LevelClear();
            Managers.Skill.ResetSkill();
            MainUI.ShowNextStage();
        }

        Score += score;
        MainUI.SetScoreUI(Score);
    }

    public void LifeDown(GameObject ball)
    {
        CurrentBalls.Remove(ball);

        if (CurrentBalls.Count != 0) return;
        if (MainUI == null)
        {
            InstanceBall();
            return;
        }

        Life--;
        MainUI.SetLifeUI(true, Life);

        if (Life == 0)
        {
            State = GameState.Pause;
            MainUI.ShowGameOver();
            Managers.Skill.ResetSkill();
        }
        else
        {
            InstanceBall();
        }
    }

    public void LevelClear()
    {
        CurrentLevel++;

        if (CurrentLevel > PlayerPrefs.GetInt("LevelsUnlocked", 0))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", CurrentLevel);
        }
    }

    public void LifeUp()
    {
        Life = Mathf.Clamp(Life, 0, 2);
        MainUI.SetLifeUI(false, Life);
        Life++;
    }
}
