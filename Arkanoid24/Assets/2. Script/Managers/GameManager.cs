using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Properties

    public MainScene Main { get; private set; }
    public MainSceneUI MainUI { get; private set; }
    public GameState State { get; set; }
    public GameMode Mode { get; set; } = GameMode.Main;
    public StageBlueprint[] Stages { get; private set; }
    public int CurrentLevel { get; set; }
    public int Bricks { get; set; }
    public int Life { get; set; }

    public bool IsMulti { get; set; } = false;

    #endregion

    #region Properties - Mode

    public float Timer { get; set; }
    public float BestRecord { get; set; }
    public float Score { get; set; }
    public float BestScore { get; set; }

    public float[] RankScores { get; set; } = new float[5];

    #endregion

    #region Initialize

    /// <summary>
    /// Managers Awake 초기화
    /// </summary>
    public void Initialize()
    {
        Stages = Managers.Resource.GetStages();
    }

    /// <summary>
    /// 메인 씬 OnStart 초기화
    /// </summary>
    public void InitScene()
    {
        Main = Object.FindObjectOfType<MainScene>();
        MainUI = Object.FindObjectOfType<MainSceneUI>();

        //Managers.Player.ReleasePlayerObject();
        //Managers.Ball.ReleaseAll();

        State = GameState.Play;
        Bricks = Stages[CurrentLevel].Bricks;

        InitMode();
    }

    /// <summary>
    /// 게임 모드에 따른 초기화
    /// </summary>
    private void InitMode()
    {
        MainUI.StartModeUI(Mode);

        switch (Mode) 
        {
            case GameMode.TimeAttack:
                Timer = 30;
                BestRecord = PlayerPrefs.GetFloat(Data.TimeRecord, 0);
                MainUI.SetTimerUI(Timer);
                MainUI.SetBestTimeUI(BestRecord);
                break;
            case GameMode.Infinity:
                BestScore = PlayerPrefs.GetFloat(Data.BestScore, 0);
                MainUI.SetScoreUI(Score);
                MainUI.SetBestScoreUI(BestScore);
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

    public void AddScore(float score)
    {
        Bricks--;
        if (MainUI == null) return;
        if (Bricks == 0)
        {
            State = GameState.Pause;
            Managers.Skill.ResetSkill(true);
            GameClearMode();
        }

        Score += score;
        MainUI.SetScoreUI(Score);
        
        // 무한 모드일 경우, 최고 점수 기록
        if (Mode != GameMode.Infinity) return;

        if (Score >= BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetFloat(Data.BestScore, BestScore);
            MainUI.SetBestScoreUI(BestScore);
        }
    }

    public void LifeUp()
    {
        Life = Mathf.Clamp(Life, 0, 2);
        MainUI.SetLifeUI(false, Life);
        Life++;
    }

    public void LifeDown(GameObject player)
    {
        if (Managers.Ball.GetBallsForPlayer(player).Count != 0) return;
        Managers.Skill.BallIncreaseSpeed = 0f;

        if (MainUI == null)
        {
            Managers.Ball.CreateBallForPlayer(player);
            return;
        }

        Life--;
        MainUI.SetLifeUI(true, Life);

        if (Life == 0)
        {
            GameOver();
        }
        else
        {
            Managers.Ball.CreateBallForPlayer(player);
        }
    }

    #endregion

    #region Game Result Methods

    /// <summary>
    /// 게임 모드에 따른 게임 결과 기능
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
                if (Timer > BestRecord)
                {
                    BestRecord = Timer;
                    PlayerPrefs.SetFloat(Data.TimeRecord, BestRecord);
                }

                MainUI.SetResultRecordUI(Timer, BestRecord);
                MainUI.ShowTimeAttack();
                break;
            // 무한 모드 => 팝업 생성 없이 바로 다음 레벨 진행
            case GameMode.Infinity:
                LevelClear();
                if (CurrentLevel >= 10) CurrentLevel = 0;
                SceneLoader.Instance.ChangeScene("Main");
                break;
            case GameMode.Versus:
                break;
        }
    }

    /// <summary>
    /// 일반적으로 게임 오버 팝업 생성
    /// 무한 모드는 랭킹 팝업으로 생성
    /// </summary>
    public void GameOver()
    {
        State = GameState.Pause;

        if (Mode == GameMode.Infinity)
        {
            Ranking();
            MainUI.SetRankingUI();
            MainUI.ShowRanking();
        }
        else
        {
            MainUI.ShowGameOver();
        }

        Managers.Skill.ResetSkill(true);
    }

    public void LevelClear()
    {
        CurrentLevel++;
        if (Mode != GameMode.Main) return;
        if (CurrentLevel > PlayerPrefs.GetInt(Data.LevelUnlock, 0))
        {
            PlayerPrefs.SetInt(Data.LevelUnlock, CurrentLevel);
        }
    }

    public void Ranking()
    {
        for (int i = 0; i < RankScores.Length; i++)
        {
            RankScores[i] = PlayerPrefs.GetFloat($"Ranks{i}", 0);

            while (RankScores[i] < Score)
            {
                var tmpScore = RankScores[i];
                RankScores[i] = Score;

                PlayerPrefs.SetFloat($"Ranks{i}", Score);
                Score = tmpScore;
            }
        }
    }

    #endregion
}
