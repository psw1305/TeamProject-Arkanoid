using UnityEngine;

public class GameScene : MonoBehaviour
{
    private EdgeCollider2D screenEdge;

    private void Start()
    {
        screenEdge = GetComponent<EdgeCollider2D>();
        screenEdge.GenerateCameraBounds();

        Managers.Game.InstanceBall();
    }
}
