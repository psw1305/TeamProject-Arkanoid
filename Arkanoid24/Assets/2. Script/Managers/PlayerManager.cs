
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager
{
    #region Member Variables

    private List<GameObject> _players = new();

    private readonly Vector3 MainCameraSpawnPoint = new Vector3(0, 0, -CameraZOrder);
    private readonly Vector3 Player1CameraSpawnPoint = new Vector3(-CameraSplitAxisX, 0, -CameraZOrder);
    private readonly Vector3 Player2CameraSpawnPoint = new Vector3(CameraSplitAxisX, 0, -CameraZOrder);

    private readonly Vector3 PlayerPaddleSpawnPoint = new Vector3(0, PaddleStartPosY, 0);
    private readonly Vector3 Player1PaddleSpawnPoint = new Vector3(-CameraSplitAxisX, PaddleStartPosY, 0);
    private readonly Vector3 Player2PaddleSpawnPoint = new Vector3(CameraSplitAxisX, PaddleStartPosY, 0);

    // Literals
    public const float CameraZOrder = 10;
    public const float CameraSplitAxisX = 10;

    public const float PaddleStartPosY = -4f;

    public const string PlayerPrefabName = "Paddle";
    public const string CameraPrefabName = "MainCamera";

    public const string TagPlayer = "Player";
    public const string TagPlayer1 = "Player1";
    public const string TagPlayer2 = "Player2";

    #endregion


    #region Player Spawner
    
    public void PlayerSpawn()
    {
        Action playerSpawn = (Managers.Game.Mode == GameMode.Versus) ?
            MultiPlayerSpawn : SoloPlayerSpawn;

        playerSpawn?.Invoke();
    }

    private void MultiPlayerSpawn()
    {
        var player1 = Managers.Resource.Instantiate(PlayerPrefabName);
        var player2 = Managers.Resource.Instantiate(PlayerPrefabName);

        player1.transform.position = Player1PaddleSpawnPoint;
        player2.transform.position = Player2PaddleSpawnPoint;

        player1.tag = TagPlayer1;
        player2.tag = TagPlayer2;

        MultiPlayerSpriteSetting(player1, player2);

        _players.Add(player1);
        _players.Add(player2);
    }

    private void MultiPlayerSpriteSetting(GameObject player1, GameObject player2)
    {
        var p1SpriteRenderer = player1.GetComponentInChildren<SpriteRenderer>();
        var p2SpriteRenderer = player2.GetComponentInChildren<SpriteRenderer>();

        p1SpriteRenderer.material = Managers.Resource.GetMaterial("BlueGlow_Paddle");
        p2SpriteRenderer.material = Managers.Resource.GetMaterial("RedGlow_Paddle");
    }

    private void SoloPlayerSpawn()
    {
        var player = Managers.Resource.Instantiate(PlayerPrefabName);

        player.transform.position = PlayerPaddleSpawnPoint;

        player.tag = TagPlayer;

        _players.Add(player);
    }

    #endregion



    #region Camera Spawner
    public void CameraSpawn()
    {
        Action cameraSpawn = (Managers.Game.Mode == GameMode.Versus) ?
            MultiCameraSpawn : SoloCameraSpawn;

        cameraSpawn?.Invoke();
    }

    private void MultiCameraSpawn()
    {
        var player1CamPrefab = Managers.Resource.Instantiate(CameraPrefabName);
        var player2CamPrefab = Managers.Resource.Instantiate(CameraPrefabName);

        player1CamPrefab.transform.position = Player1CameraSpawnPoint;
        player2CamPrefab.transform.position = Player2CameraSpawnPoint;

        // Audio Listener Remove
        AudioListener audioListener2 = player2CamPrefab.GetComponent<AudioListener>();
        if(audioListener2 != null)
            GameObject.Destroy(audioListener2);

        var player1Camera = player1CamPrefab.GetComponent<Camera>();
        var player2Camera = player2CamPrefab.GetComponent<Camera>();

        Rect camera1ViewRect = new Rect(0, 0, 0.5f, 1);
        Rect camera2ViewRect = new Rect(0.5f, 0, 0.5f, 1);

        player1Camera.rect = camera1ViewRect;
        player2Camera.rect = camera2ViewRect;
    }

    private void SoloCameraSpawn()
    {
        var mainCamera = Managers.Resource.Instantiate(CameraPrefabName);

        mainCamera.transform.position = MainCameraSpawnPoint;

        mainCamera.AddComponent<CameraSetter>();
    }
    #endregion


    #region Utilites
    // 현재 활성화된 플레이어(들)을 반환하는 메서드
    public List<GameObject> GetActivePlayers()
    {
        // 멀티플레이 모드일 경우
        if (Managers.Game.Mode == GameMode.Versus)
        {
            return _players;
        }
        // 솔로 플레이 모드일 경우
        else
        {
            // 리스트에 하나의 플레이어만 포함되어 있는지 확인
            if (_players.Count > 0)
            {
                // 첫 번째 플레이어만 반환
                return new List<GameObject> { _players[0] };
            }
            else
            {
                // 플레이어가 없을 경우, 빈 리스트 반환
                return new List<GameObject>();
            }
        }
    }

    public void ReleasePlayerObject()
    {
        _players.Clear();
    }
    #endregion
}
