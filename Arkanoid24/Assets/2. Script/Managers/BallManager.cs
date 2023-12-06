
using System.Collections.Generic;
using UnityEngine;

public class BallManager
{
    private Dictionary<GameObject, List<GameObject>> _playerBallMap =
        new Dictionary<GameObject, List<GameObject>>();


    public void AssignBallToPlayer(GameObject player, GameObject ball)
    {
        // 해당 플레이어에 대한 볼 리스트가 없다면?
        if (!_playerBallMap.ContainsKey(player))
        {
            _playerBallMap[player] = new List<GameObject>();
        }

        // 볼 리스트에 추가
        _playerBallMap[player].Add(ball);
    }

    public List<GameObject> GetBallsForPlayer(GameObject player)
    {
        if(_playerBallMap.TryGetValue(player, out List<GameObject> balls))
        {
            return balls;
        }

        return new List<GameObject>(); // 없을 경우 빈 오브젝트 리스트를 반환
    }

    public void RemovePlayer(GameObject player)
    {
        if(_playerBallMap.ContainsKey(player))
        {
            _playerBallMap.Remove(player);
        }
    }

    public void RemoveBallFromPlayer(GameObject player, GameObject ball)
    {
        if(_playerBallMap.ContainsKey(player))
        {
            _playerBallMap[player].Remove(ball);
        }
    }

    public void ReleaseAll()
    {
        _playerBallMap.Clear();
    }
}
