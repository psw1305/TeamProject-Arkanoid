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
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
