
using UnityEngine;

public class VersusScene : MonoBehaviour
{
    private VersusLevelBlueprint versusLevelBlueprint;

    #region MonoBehaviour

    private void Start()
    {
        ReleasePrevData();
        InitMainGame();
    }

    #endregion

    private void InitMainGame()
    {
        // #1. Manager / Player Setting
        Managers.Versus.InitScene();
        Managers.Player.CameraSpawn();
        Managers.Player.PlayerSpawn();

        // #2. Scene Loading
        SceneLoader.Instance.OnSceneLoaded();

        // #3. Create Current Level
        CreateStage();

        // # 4. Ball Create To Assgin Player
        Managers.Ball.CreateBalls();
    }

    private void ReleasePrevData()
    {
        Managers.Player.ReleasePlayerObject();
        Managers.Ball.ReleaseAll();
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
