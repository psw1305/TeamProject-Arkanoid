using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttackButton : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI recordText;

    private void Awake()
    {
        button.onClick.AddListener(LevelStart);
    }

    private void LevelStart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        Managers.Game.CurrentLevel = 0;
        Managers.Game.Mode = GameMode.TimeAttack;
        SceneLoader.Instance.ChangeScene("Main");
    }
}
