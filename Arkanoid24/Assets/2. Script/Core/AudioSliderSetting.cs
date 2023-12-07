using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioSliderSetting : MonoBehaviour
{
    public Slider audioSlider;

    public void SFXAuidoControl(float sound)
    {
        sound = audioSlider.value; // 0.0001 ~ 1
        AudioSystem<SFX>.Instance.VolumeScale = sound;
        PlayerPrefs.SetFloat("SFXSoundValue", sound);
    }

    public void BGMAuidoControl(float sound)
    {
        sound = audioSlider.value; // 0.0001 ~ 1
        AudioSystem<BGM>.Instance.VolumeScale = sound;
        PlayerPrefs.SetFloat("BGMSoundValue", sound);
    }
}
