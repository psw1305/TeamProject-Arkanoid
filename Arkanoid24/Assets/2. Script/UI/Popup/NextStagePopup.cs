using UnityEngine;
using UnityEngine.UI;

public class NextStagePopup : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        nextButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(Menu);
    }

    private void NextLevel()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        if (Managers.Game.CurrentLevel >= 10)
        {
            SceneLoader.Instance.ChangeScene("Lobby");
        }
        else
        {
            SceneLoader.Instance.ChangeScene("Main");
        }
    }

    private void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneLoader.Instance.ChangeScene("Lobby");
    }
}
