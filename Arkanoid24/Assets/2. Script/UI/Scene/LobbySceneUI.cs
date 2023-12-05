using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneUI : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject[] menus;
    [SerializeField] private Button levelButtons;
    [SerializeField] private Button settingsButtons;
    [SerializeField] private Button creditsButtons;

    [Header("Level")]
    [SerializeField] private LevelButton[] levels;
    [SerializeField] private GameObject[] specialLevels;

    [Header("Settings")]
    [SerializeField] private Button dataClearButton;
    [SerializeField] private AudioSliderSetting SFXAudioSliderSetting;
    [SerializeField] private AudioSliderSetting BGMAudioSliderSetting;

    [Header("Others")]
    [SerializeField] private Button[] backButtons;

    AudioSliderSetting audioSliderSetting;

    private void Awake()
    {
        // #1. 메뉴 버튼 메서드 활성화
        levelButtons.onClick.AddListener(delegate { OpenMenu(1); });
        settingsButtons.onClick.AddListener(delegate { OpenMenu(2); });
        creditsButtons.onClick.AddListener(delegate { OpenMenu(3); });

        // #2. 백 버튼 초기화
        foreach (var backBtn in backButtons)
        {
            backBtn.onClick.AddListener(delegate { OpenMenu(0); });
        }

        dataClearButton.onClick.AddListener(DataClear);

        // #3. 저장 데이터 체크
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 0);
        }
    }

    private void Start()
    {
        // 오디오볼륨 저장 데이터 확인
        if (!PlayerPrefs.HasKey("SFXSoundValue"))
        {
            PlayerPrefs.SetFloat("SFXSoundValue", 1f);
        }
        else
        {
            //SFX
            SFXAudioSliderSetting.audioSlider.value = PlayerPrefs.GetFloat("SFXSoundValue");
            SFXAudioSliderSetting.SFXAuidoControl(SFXAudioSliderSetting.audioSlider.value);

            //BGM
            BGMAudioSliderSetting.audioSlider.value = PlayerPrefs.GetFloat("BGMSoundValue");
            BGMAudioSliderSetting.BGMAuidoControl(BGMAudioSliderSetting.audioSlider.value);
        }
    }

    private void OpenMenu(int menuIndex)
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        foreach (var menu in menus) 
        {
            menu.SetActive(false);
        }

        menus[menuIndex].SetActive(true);
    }

    private void DataClear()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);

        PlayerPrefs.DeleteAll();
    }
}
