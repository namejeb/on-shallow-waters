using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundFile 
{
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)] public float volume = 0.5f;


  
}
