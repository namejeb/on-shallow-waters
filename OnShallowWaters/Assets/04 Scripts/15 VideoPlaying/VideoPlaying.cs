using UnityEngine;
using UnityEngine.Video;

public class VideoPlaying : MonoBehaviour
{
    private VideoPlayer _vp;

    public VideoPlayer VP
    {
        get => _vp;
    }

    private void OnDestroy()
    {
        _vp.loopPointReached -= EndPointReached;
    }
    
    private void Awake()
    {
        _vp = GetComponent<VideoPlayer>();
   
        _vp.loopPointReached += EndPointReached;
    }

    private void EndPointReached(VideoPlayer vp)
    {
        EndVideo();
    }

    protected void EndVideo(){
        //end video logic
    }
}