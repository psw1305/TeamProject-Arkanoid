using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeAttackPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(Menu);
    }

    private void Restart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Main");
    }

    private void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Lobby");
    }
}
