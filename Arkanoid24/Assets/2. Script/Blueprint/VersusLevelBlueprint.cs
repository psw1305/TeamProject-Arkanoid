using UnityEngine;

[CreateAssetMenu(fileName = "VersusStage", menuName = "Blueprint/VersusStage")]
public class VersusLevelBlueprint : ScriptableObject
{
    [SerializeField] private int level;
    [SerializeField] private GameObject stageMap;
    [SerializeField] private int player1Bricks;
    [SerializeField] private int player2Bricks;

    public int Level => level;
    public GameObject StageMap => stageMap;
    public int Player1Bricks => player1Bricks;
    public int Player2Bricks => player2Bricks;
}
