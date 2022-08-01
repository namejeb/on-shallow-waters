using UnityEngine;


public class SkipableVideo : VideoPlaying
{
    private void Update()
    {
        //make sure to have "SkipVideo" array element in InputManager
        if ( Input.GetMouseButtonDown(0) )
        {
            EndVideo();
        }
    }
}


