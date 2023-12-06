using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FirstscoreText;
    [SerializeField] private TextMeshProUGUI SecondscoreText;
    [SerializeField] private TextMeshProUGUI ThirdscoreText;
    [SerializeField] private TextMeshProUGUI FourthscoreText;
    [SerializeField] private TextMeshProUGUI FifthscoreText;

    private float firstscore = 0;   
    private float secondscore = 0;
    private float thirdscore = 0;
    private float fourthscore = 0;
    private float fifthscore = 0;

    private float[] RankScores = new float[4];

    void RankSetting()
    {
        float rankscore = 0;
        rankscore = Managers.Game.Score;
        PlayerPrefs.SetFloat("Ranks", rankscore);
        float tmpScore = 0f;

        for (int i = 0; i < 4; i++)
        {
            RankScores[i] = PlayerPrefs.GetFloat("Ranks");

            while (RankScores[i] < rankscore)
            {
                tmpScore = RankScores[i];
                RankScores[i] = rankscore;

                PlayerPrefs.SetFloat("Ranks", rankscore);

                rankscore = tmpScore;
            }
        }

        firstscore = RankScores[0];
        FirstscoreText.text = $"{firstscore}"; 

        secondscore = RankScores[1];
        SecondscoreText.text = $"{secondscore}";

        thirdscore = RankScores[2];
        ThirdscoreText.text = $"{thirdscore}";

        fourthscore = RankScores[3];
        FourthscoreText.text = $"{fourthscore}";

        fifthscore = RankScores[4];
        FifthscoreText.text = $"{fifthscore}";
    }

    private void Update()
    {

    }

}
