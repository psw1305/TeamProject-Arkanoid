using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        returnButton.onClick.AddListener(Return);
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(Menu);
    }

    private void Return()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        Managers.Game.State = GameState.Play;
        gameObject.SetActive(false);
    }

    private void Restart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        if (Managers.Game.Mode == GameMode.Infinity)
        {
            Managers.Game.Life = 3;
            Managers.Game.Score = 0;
            Managers.Game.CurrentLevel = 0;
        }

        SceneLoader.Instance.ChangeScene("Main");
    }

    private void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Lobby");
    }
}
