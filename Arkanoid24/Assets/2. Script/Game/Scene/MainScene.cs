using System.Collections;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    private StageBlueprint stageBlueprint;

    #region MonoBehaviour

    private void Start()
    {
        InitMainGame();
    }

    #endregion

    private void InitMainGame()
    {
        // #1. 매니저 세팅
        Managers.Game.InitScene();

        // #2. 씬 로딩
        SceneLoader.Instance.OnSceneLoaded();

        // #3. 현재 레벨에 맞는 스테이지 생성
        CreateStage();

        // #4. 게임 모드 세팅
        GameModeSetting();

        // #5. 공 생성 후 대기
        Managers.Game.InstanceBall();
    }

    /// <summary>
    /// 설계도에서 받은 스테이지 맵 생성
    /// </summary>
    private void CreateStage()
    {
        stageBlueprint = Managers.Game.GetCurrentStage();
        Instantiate(stageBlueprint.StageMap);
    }

    /// <summary>
    /// 게임 모드에 따른 세팅
    /// </summary>
    private void GameModeSetting()
    {
        switch (Managers.Game.Mode)
        {
            case GameMode.Main:
                Managers.Game.Life = 3;
                Managers.Game.Score = 0;
                break;

            case GameMode.TimeAttack:
                Managers.Game.Life = 3;
                StartCoroutine(StartTimer());
                break;

            case GameMode.Infinity:
                break;

            case GameMode.Versus:
                break;
        }
    }

    /// <summary>
    /// 타이머 시작, 타이머 도달 시 => 게임 오버
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTimer()
    {
        while (Managers.Game.Timer > 0)
        {
            if (Managers.Game.State != GameState.Play) break;
            Managers.Game.Timer -= Time.deltaTime;
            Managers.Game.MainUI.SetTimerUI(Managers.Game.Timer);
            yield return null;
        }

        if (Managers.Game.Timer <= 0)
        {
            Managers.Game.Timer = 0;
            Managers.Game.MainUI.SetTimerUI(0);
            Managers.Game.GameOver();
        }
    }
}
