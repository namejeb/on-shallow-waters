using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;
    
    
    const string MIXER_Master = "MasterVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_Music = "BGMVolume";
    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        bgmSlider.onValueChanged.AddListener(SetMusicVolume);
    }
   
 
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_Music, Mathf.Log10(value)* 25);
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 25);
    }
    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_Master, Mathf.Log10(value) * 25);
    }
}
