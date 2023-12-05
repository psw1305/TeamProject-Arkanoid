
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    #region Member Variables

    private List<GameObject> _players = new();
    private List<GameObject> _cameras = new();

    private Vector2 playerSpawnPoint;

    // Literals
    public const string PlayerPrefabName = "Paddle";
    public const string CameraPrefabName = "MainCamera";

    public const string TagPlayer = "Player";
    public const string TagPlayer1 = "Player1";
    public const string TagPlayer2 = "Player2";

    #endregion


    #region Player Spawner
    
    public void PlayerSpawn()
    {
        Action playerSpawn = (Managers.Game.IsMulti == true) ?
            MultiPlayerSpawn : SoloPlayerSpawn;

        playerSpawn?.Invoke();
    }

    private void MultiPlayerSpawn()
    {
        var player1 = Managers.Resource.Instantiate(PlayerPrefabName, new Vector2(0, -4));
        var player2 = Managers.Resource.Instantiate(PlayerPrefabName, new Vector2(2, -4));

        player1.tag = TagPlayer1;
        player2.tag = TagPlayer2;

        _players.Add(player1);
        _players.Add(player2);
    }

    private void SoloPlayerSpawn()
    {
        var player = Managers.Resource.Instantiate(PlayerPrefabName, new Vector2(0, -4));

        player.tag = TagPlayer;

        _players.Add(player);
    }

    #endregion



    #region Camera Spawner
    public void CameraSpawn()
    {

    }

    private void MultiCameraSpawn()
    {
        var player1Camera = Managers.Resource.Instantiate(CameraPrefabName, new Vector2(-10, 0));
        var player2Camera = Managers.Resource.Instantiate(CameraPrefabName, new Vector2(10, 0));
    }

    private void SoloCameraSpawn()
    {

    }
    #endregion


    #region Utilites
    public void ReleasePlayerObject()
    {
        _players.Clear();
    }
    #endregion
}
