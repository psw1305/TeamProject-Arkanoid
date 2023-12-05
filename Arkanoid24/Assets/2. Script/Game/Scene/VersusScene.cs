using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class VersusScene : MonoBehaviour
{
    private VersusLevelBlueprint versusLevelBlueprint;
    private EdgeCollider2D screenEdge;

    #region MonoBehaviour

    private void Awake()
    {
        Managers.Resource.Initialize();
        Managers.Versus.Initialize();

        screenEdge = GetComponent<EdgeCollider2D>();
        screenEdge.GenerateCameraBounds();
    }

    private void Start()
    {
        InitMainGame();
    }

    #endregion

    private void InitMainGame()
    {
        // #1. 현재 레벨에 맞는 스테이지 생성
        CreateStage();

        Managers.Player.PlayerSpawn();

        // #2. 공 생성 후 대기
        Managers.Versus.InstanceBall();
    }

    /// <summary>
    /// 설계도에서 받은 스테이지 맵 생성
    /// </summary>
    private void CreateStage()
    {
        versusLevelBlueprint = Managers.Versus.CurrentStage();
        Instantiate(versusLevelBlueprint.StageMap);
    }
}
