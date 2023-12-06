
using UnityEngine;
using UnityEngine.UI;

public class VersusMultiButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(LevelStart);
    }

    private void LevelStart()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        Managers.Game.Mode = GameMode.Versus;
        SceneLoader.Instance.ChangeScene("Versus");
    }
}