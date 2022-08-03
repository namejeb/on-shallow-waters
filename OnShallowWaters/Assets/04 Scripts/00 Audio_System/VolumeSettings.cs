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
    
    
    public const string MIXER_Master = "MasterVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_Music = "BGMVolume";
    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        bgmSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void Start()
    {
       masterSlider.value = PlayerPrefs.GetFloat(SoundManager.MASTER_KEY,1.1f);
       sfxSlider.value = PlayerPrefs.GetFloat(SoundManager.SFX_KEY, 1.1f);
       bgmSlider.value = PlayerPrefs.GetFloat(SoundManager.MUSIC_KEY, 1.1f);
    }



    void OnDisable()
    {
        PlayerPrefs.SetFloat(SoundManager.MASTER_KEY,masterSlider.value);
        PlayerPrefs.SetFloat(SoundManager.SFX_KEY, sfxSlider.value);
        PlayerPrefs.SetFloat(SoundManager.MUSIC_KEY, bgmSlider.value);
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
