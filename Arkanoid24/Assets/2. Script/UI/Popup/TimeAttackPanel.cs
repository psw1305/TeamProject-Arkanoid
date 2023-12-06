using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeAttackPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(Menu);
    }

    public void Restart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Main");
    }

    public void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Lobby");
    }
}
