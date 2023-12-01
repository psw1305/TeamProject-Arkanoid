using UnityEngine;
using TMPro;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] lifes;

    [Header("Popup")]
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject nextStagePopup;

    public void SetScoreUI(float score)
    {
        scoreText.text = $"Á¡¼ö : {score}";
    }

    public void SetLifeUI(bool isDown, int num)
    {
        if (isDown)
        {
            lifes[num].SetActive(false);
        }
        else
        {
            lifes[num].SetActive(true);
        }
    }

    public void ShowGameOver()
    {
        gameOverPopup.SetActive(true);
    }

    public void ShowNextStage()
    {
        nextStagePopup.SetActive(true);
    }
}
