using UnityEngine;
using UnityEngine.UI;

public class NextPanel : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        nextButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(Menu);
    }

    public void NextLevel()
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
