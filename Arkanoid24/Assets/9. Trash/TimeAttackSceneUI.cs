using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAttackSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI BestscoreText;
    [SerializeField] private GameObject[] lifes;

    private float _timer;
    public TextMeshProUGUI timerText;

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

    // 최고 기록 (유건희)
    private string KeyName = "BestScoreTA";
    private float bestscore = 0;

    void Start()
    {
        bestscore = PlayerPrefs.GetFloat(KeyName, 0);
        BestscoreText.text = $"최고 기록 : {bestscore.ToString()}";

        //_timer = Managers.Game.Time[Managers.Game.CurrentLevel];

        StartCoroutine(TimerStart());
    }

    private void Update()
    {
        if (Managers.Game.Score > bestscore)
        {
            PlayerPrefs.SetFloat(KeyName, Managers.Game.Score);
        }

    }

    private IEnumerator TimerStart()
    {
        while (_timer > 0)
        {
            if (Managers.Game.State != GameState.Play) break;
            _timer -= Time.deltaTime;
            timerText.text = string.Format(_timer.ToString("F2"));
            yield return null;
        }
        if (_timer <= 0)
        {
            _timer = 0;
            timerText.text = string.Format(_timer.ToString("F2"));
            Managers.Game.GameOver();
        }
    }
}
