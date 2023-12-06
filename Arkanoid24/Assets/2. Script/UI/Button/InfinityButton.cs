using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfinityButton : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        button.onClick.AddListener(LevelStart);
    }

    private void LevelStart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        Managers.Game.Life = 3;
        Managers.Game.CurrentLevel = 0;
        Managers.Game.Mode = GameMode.Infinity;
        SceneLoader.Instance.ChangeScene("Main");
    }
}
