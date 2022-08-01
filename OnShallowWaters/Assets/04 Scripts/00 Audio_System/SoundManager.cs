using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource SFX_Source, BGM_Source;


    [SerializeField] float bgmVolume;
    [SerializeField] float sfxVolume;

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        SFX_Source.volume = sfxVolume;
    }

    public void SetBGMVolume()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        BGM_Source.volume = bgmVolume;
    }

    SoundFile GetSound(SoundData _soundType, string _name)
    {
        List<SoundFile> temp = new List<SoundFile>(_soundType.SoundList);

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].name == _name)
            {
                return temp[i];
            }
        }
        
        return null;
    }

    public void PlaySFX(SoundData _soundData, string _name)
    {
        
        SoundFile sound = GetSound(_soundData, _name);
        SFX_Source.outputAudioMixerGroup = _soundData.mixer;
        if (sound != null)
        {
            SFX_Source.PlayOneShot(sound.clip,sound.volume);
        }
    }

    public void PlayBGM(SoundData _soundData, string _name, bool loop = true)
    {
        SoundFile sound = GetSound(_soundData, _name);
        if (sound != null)
        {
            BGM_Source.volume = sound.volume;
            BGM_Source.clip = sound.clip;
            BGM_Source.loop = loop;

            BGM_Source.Play();
        }
    }

    public void StopBGM()
    {
        BGM_Source.Stop();
    }

    public void StopAllSound()
    {
        SFX_Source.Stop();
        BGM_Source.Stop();
    }
}