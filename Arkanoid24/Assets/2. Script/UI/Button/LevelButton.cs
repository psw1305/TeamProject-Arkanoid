using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Image board;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(LevelStart);
    }

    /// <summary>
    /// 설정에서 데이터 체크 확인용
    /// </summary>
    private void OnEnable()
    {
        // 언락 레벨보다 낮을 경우, 버튼 잠김
        if (level <= PlayerPrefs.GetInt("LevelsUnlocked"))
        {
            button.enabled = true;
            board.color = Color.white;
        }
        else
        {
            button.enabled = false;
            board.color = Color.black;
        }
    }

    /// <summary>
    /// 해당 레벨 플레이
    /// </summary>
    private void LevelStart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        Managers.Game.Mode = GameMode.Main;
        Managers.Game.CurrentLevel = level;
        SceneLoader.Instance.ChangeScene("Main");
    }
}
