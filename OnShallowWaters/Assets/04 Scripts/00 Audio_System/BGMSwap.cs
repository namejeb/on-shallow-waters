using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwap : MonoBehaviour
{
    public AudioClip newTrack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.SwapTrack(newTrack);
        }
    }


}
