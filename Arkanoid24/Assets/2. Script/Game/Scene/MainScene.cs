using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainScene : MonoBehaviour
{
    private StageBlueprint stageBlueprint;
    private EdgeCollider2D screenEdge;

    #region MonoBehaviour

    private void Awake()
    {
        Managers.Resource.Initialize();
        Managers.Game.Initialize();

        screenEdge = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        InitMainGame();
    }

    #endregion

    private void InitMainGame()
    {
        // #1. 라이프 3개로 설정 
        Managers.Game.Life = 3;

        // #2. 스코어 0점으로 시작
        //Managers.Game.Score = 0;

        Managers.Player.CameraSpawn();

        // #3. 현재 레벨에 맞는 스테이지 생성
        CreateStage();

        Managers.Player.PlayerSpawn();

        // #4. 공 생성 후 대기
        Managers.Game.InstanceBall();
    }

    /// <summary>
    /// 설계도에서 받은 스테이지 맵 생성
    /// </summary>
    private void CreateStage()
    {
        stageBlueprint = Managers.Game.CurrentStage();
        Instantiate(stageBlueprint.StageMap);
    }
}
