using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using JetBrains.Annotations;

public class VersusSceneUI : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject player1UI;
    [SerializeField] private GameObject player2UI;


    public void ShowGameOver(string winPlayer)
    {
        //BGM.Instance.Play(BGM.Instance.gameover, false);

        if (winPlayer == "Player1")
        {
            player1UI.SetActive(true);
            player2UI.SetActive(false);
        }
        else
        {
            player1UI.SetActive(false);
            player2UI.SetActive(true);
        }

        gameOverPopup.SetActive(true);
    }
}
