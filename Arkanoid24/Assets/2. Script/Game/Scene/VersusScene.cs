
using UnityEngine;

public class VersusScene : MonoBehaviour
{
    private VersusLevelBlueprint versusLevelBlueprint;

    #region MonoBehaviour

    private void Awake()
    {
        Managers.Resource.Initialize();
        Managers.Versus.Initialize();


    }

    private void Start()
    {
        InitMainGame();
    }

    #endregion

    private void InitMainGame()
    {
        // #0. 씬 로딩
        SceneLoader.Instance.OnSceneLoaded();

        // # 1. Setting (카메라, 플레이어, 스테이지)
        Managers.Player.CameraSpawn();
        Managers.Player.PlayerSpawn();
        CreateStage();

        // #2. 공 생성 후 대기
        // Managers.Versus.InstanceBall();
        Managers.Versus.InstanceBall(VersusManager.player1Index);
        Managers.Versus.InstanceBall(VersusManager.player2Index);
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
