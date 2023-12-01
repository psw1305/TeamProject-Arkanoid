using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextPanel : MonoBehaviour
{
    [SerializeField] private Button nextButton;

    private void Start()
    {
        nextButton.onClick.AddListener(Restart);
    }

    public void Restart()
    {
        Managers.Game.CurrentLevel++;
        SceneManager.LoadScene("Main");
    }
}
