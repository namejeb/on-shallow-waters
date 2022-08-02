using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlaying_Cutscene : SkipableVideo
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
}
