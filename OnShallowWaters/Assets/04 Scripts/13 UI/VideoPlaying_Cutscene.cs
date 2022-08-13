using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlaying_Cutscene : VideoPlaying
{
    [SerializeField] private Transform loadingScreen;


    private void Start()
    {
        GameManager.SetIsFirstPlayThrough(false);
    }

    protected override void EndVideo()
    {
        loadingScreen.gameObject.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    // called in Skip button
    public void End()
    {
        EndVideo();
    }
}
