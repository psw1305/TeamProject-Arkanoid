using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioSliderSetting : MonoBehaviour
{
    //[SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;

    public void SFXAuidoControl()
    {
        float sound = audioSlider.value; // 0.0001 ~ 1

        AudioSystem<SFX>.Instance.VolumeScale = sound;
    }

    public void BGMAuidoControl()
    {
        float sound = audioSlider.value; // 0.0001 ~ 1

        AudioSystem<BGM>.Instance.VolumeScale = sound;
    }
}
