using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Blueprint/Stage")]
public class StageBlueprint : ScriptableObject
{
    [SerializeField] private int level;
    [SerializeField] private GameObject stageMap;
    [SerializeField] private int bricks;

    public int Level => level;
    public GameObject StageMap => stageMap;
    public int Bricks => bricks;
}
