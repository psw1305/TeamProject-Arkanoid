using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOverPanel : MonoBehaviour
{
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
        Managers.Game.Score = 0;
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene("Lobby");
    }
}
