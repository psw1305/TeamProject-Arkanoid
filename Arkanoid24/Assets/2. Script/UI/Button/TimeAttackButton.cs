using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeAttackButton : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(LevelStart);
    }

    private void LevelStart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        Managers.Game.CurrentLevel = 0;
        Managers.Game.Mode = GameMode.TimeAttack;
        SceneManager.LoadScene("TimeAttack");
    }
}
