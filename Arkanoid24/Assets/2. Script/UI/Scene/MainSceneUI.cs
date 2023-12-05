using UnityEngine;
using TMPro;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject[] lifes;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI BestscoreText;

    [Header("Popup")]
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject nextStagePopup;
    [SerializeField] private GameObject RankPopup;

    public void SetScoreUI(float score)
    {
        scoreText.text = $"점수 : {score}";
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
        SFX.Instance.PlayOneShot(SFX.Instance.gameover);

        gameOverPopup.SetActive(true);
    }

    public void ShowNextStage()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.nextstage);

        nextStagePopup.SetActive(true);
    }

    // 클래식 모드_Main_우측 상단의 최고 기록
    private string KeyName = "BestScore";
    private float bestscore = 0;

    void Start()
    {
        bestscore = PlayerPrefs.GetFloat(KeyName, 0);
        BestscoreText.text = $"최고 기록 : {bestscore}";
    }

    private void LateUpdate()
    {
        if (Managers.Game.Score > bestscore)
        {
            PlayerPrefs.SetFloat(KeyName, Managers.Game.Score);
        }
    }

}
