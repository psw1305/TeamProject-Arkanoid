using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private StageBlueprint stageBlueprint;
    private EdgeCollider2D screenEdge;

    private void Start()
    {
        screenEdge = GetComponent<EdgeCollider2D>();
        screenEdge.GenerateCameraBounds();

        CreateStage();

        Managers.Game.InstanceBall();
    }

    private void CreateStage()
    {
        Instantiate(stageBlueprint.StageMap);
    }
}
