using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    #region Member Variables

    public List<GameObject> CurrentBalls = new();

    #endregion



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

    // Multi Play Flag
    public bool IsMulti { get; set; } = true; // Test : true
    
    public List<float> Time {  get; set; }
    public GameMode Mode { get; set; } = GameMode.Main;
    public TimeAttackSceneUI TimeAttackUI { get; private set; }

    
    #endregion

    public void Initialize()
    {
        CurrentBalls.Clear();

        Main = Object.FindObjectOfType<MainScene>();
        MainUI = Object.FindObjectOfType<MainSceneUI>();
        State = GameState.Play;
        Stages = Managers.Resource.GetStages();
        Bricks = Stages[CurrentLevel].Bricks;
        TimeAttackUI = Object.FindAnyObjectByType<TimeAttackSceneUI>();
        Time = new List<float> { 40f, 60f, 100f, 120f };
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
        if ((Mode == GameMode.Main && MainUI == null) || (Mode == GameMode.TimeAttack && TimeAttackUI == null)) return;
        if (Bricks == 0)
        {
            State = GameState.Pause;

            Managers.Skill.ResetSkill();

            LevelClear();
            // MainUI.ShowNextStage();
            if (Mode == GameMode.TimeAttack) TimeAttackUI.ShowNextStage();
            else if (Mode == GameMode.Main) MainUI.ShowNextStage();
        

        }

        Score += score;

        //MainUI.SetScoreUI(Score);
        if (Mode == GameMode.TimeAttack) TimeAttackUI.SetScoreUI(Score);
        else MainUI.SetScoreUI(Score);
    }

    public void LifeDown(GameObject ball)
    {
        CurrentBalls.Remove(ball);

        if (CurrentBalls.Count != 0) return;
        if ((Mode == GameMode.Main && MainUI == null) || (Mode == GameMode.TimeAttack && TimeAttackUI == null))

        {
            InstanceBall();
            return;
        }
        if (CurrentBalls.Count != 0) return;

        Life--;
        //MainUI.SetLifeUI(true, Life);
        if (Mode == GameMode.TimeAttack) TimeAttackUI.SetLifeUI(true, Life);
        else if (Mode == GameMode.Main) MainUI.SetLifeUI(true, Life);

        if (Life == 0)
        {

            GameOver();

        }
        else
        {
            InstanceBall();
        }
    }

    public void GameOver()
    {
        State = GameState.Pause;
        // MainUI.ShowGameOver();
        if (Mode == GameMode.TimeAttack) TimeAttackUI.ShowGameOver();
        else if (Mode == GameMode.Main) MainUI.ShowGameOver();
        Managers.Skill.ResetSkill();
    }

    public void LevelClear()
    {
        CurrentLevel++;

        if (Mode == GameMode.Main && CurrentLevel > PlayerPrefs.GetInt("LevelsUnlocked", 0))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", CurrentLevel);
        }
    }

    public void LifeUp()
    {
        Life = Mathf.Clamp(Life, 0, 2);
        //MainUI.SetLifeUI(false, Life);
        if (Mode == GameMode.TimeAttack) TimeAttackUI.SetLifeUI(false, Life);
        else if (Mode == GameMode.Main) MainUI.SetLifeUI(false, Life);
        Life++;
    }
}
