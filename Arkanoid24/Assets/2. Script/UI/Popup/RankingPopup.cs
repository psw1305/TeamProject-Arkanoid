using UnityEngine;
using UnityEngine.UI;

public class RankingPopup : MonoBehaviour
{
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
        Managers.Game.Life = 3;
        Managers.Game.CurrentLevel = 0;
        SceneLoader.Instance.ChangeScene("Main");
    }

    private void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Lobby");
    }
}
