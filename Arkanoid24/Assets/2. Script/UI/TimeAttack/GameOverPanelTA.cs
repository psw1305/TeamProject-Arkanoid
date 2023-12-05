using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelTA : MonoBehaviour
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
        SceneManager.LoadScene("TimeAttack");
    }

    public void Menu()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene("Lobby");
    }
}
