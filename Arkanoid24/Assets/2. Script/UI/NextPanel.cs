using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene("Lobby");
    }
}
