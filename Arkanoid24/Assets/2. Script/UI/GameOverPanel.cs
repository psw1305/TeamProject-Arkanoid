using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
    }

    public void Restart()
    {
        Managers.Game.Score = 0;
        SceneManager.LoadScene("Main");
    }
}
