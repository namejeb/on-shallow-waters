using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Sound/CreateSoundData")]
public class SoundData : ScriptableObject 
{
    public AudioMixerGroup mixer;
    public List<SoundFile> SoundList = new List<SoundFile>();
}
