using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Member Variables

    public List<GameObject> CurrentBalls = new();

    #endregion



    #region Properties

    public MainScene Main { get; private set; }
    public MainSceneUI MainUI { get; private set; }
    public GameState State { get; set; }
    public GameMode Mode { get; set; } = GameMode.Main;
    public StageBlueprint[] Stages { get; private set; }
    public int CurrentLevel { get; set; }
    public int Bricks { get; set; }
    public int Life { get; set; }
    public float Score { get; set; }


    // Multi Play Flag
    public bool IsMulti { get; set; } = true; // Test : true
    
    public List<float> Time {  get; set; }
    //public GameMode Mode { get; set; } = GameMode.Main;
    public TimeAttackSceneUI TimeAttackUI { get; private set; }

    
    #endregion

    #region Properties - Mode

    public float Timer { get; set; }
    public float BestScore { get; set; }

    #endregion

    #region Initialize

    /// <summary>
    /// ���� �Ŵ��� �ʱ�ȭ
    /// </summary>
    public void Initialize()
    {
        CurrentBalls.Clear();

        Main = Object.FindObjectOfType<MainScene>();
        MainUI = Object.FindObjectOfType<MainSceneUI>();
        State = GameState.Play;
        Stages = Managers.Resource.GetStages();
        Bricks = Stages[CurrentLevel].Bricks;

        InitMode();
    }

    /// <summary>
    /// ���� ��忡 ���� �ʱ�ȭ
    /// </summary>
    private void InitMode()
    {
        switch (Mode) 
        {
            case GameMode.TimeAttack:
                Timer = 20;
                MainUI.SetTimerUI(Timer);
                break;
            // ���Ѹ�� => ����, ������ ����
            case GameMode.Infinity:
                MainUI.SetScoreUI(Score);
                MainUI.SetCurrentLifeUI(Life);
                break;
            case GameMode.Versus:
                break;
        }
    }

    public StageBlueprint GetCurrentStage()
    {
        return Stages[CurrentLevel];
    }

    #endregion

    #region Game Play Methods

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

            Managers.Skill.ResetSkill();
            GameClearMode();
        }

        Score += score;
        MainUI.SetScoreUI(Score);
    }

    public void LifeUp()
    {
        Life = Mathf.Clamp(Life, 0, 2);
        MainUI.SetLifeUI(false, Life);
        Life++;
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
        if (CurrentBalls.Count != 0) return;

        Life--;
        MainUI.SetLifeUI(true, Life);

        if (Life == 0)
        {

            GameOver();

        }
        else
        {
            InstanceBall();
        }
    }

    #endregion

    #region Game Result Methods

    /// <summary>
    /// ���� ��忡 ���� Ŭ���� ����
    /// </summary>
    private void GameClearMode()
    {
        switch (Mode)
        {
            case GameMode.Main:
                LevelClear();
                MainUI.ShowNextStage();
                break;
            case GameMode.TimeAttack:
                MainUI.ShowTimeAttack();
                break;
            // Ŭ���� ��, �˾� �� ���� �ٷ�, ���� ��������
            case GameMode.Infinity:
                LevelClear();
                SceneLoader.Instance.ChangeScene("Main");
                break;
            case GameMode.Versus:
                break;
        }
    }

    public void GameOver()
    {
        State = GameState.Pause;
        MainUI.ShowGameOver();
        Managers.Skill.ResetSkill();
    }

    public void LevelClear()
    {
        CurrentLevel++;

        if (CurrentLevel > PlayerPrefs.GetInt(Data.LevelUnlock, 0))
        {
            PlayerPrefs.SetInt(Data.LevelUnlock, CurrentLevel);
        }
    }

    #endregion
}
