using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlaying_Cutscene : VideoPlaying
{
    [SerializeField] private Transform loadingScreen;


    protected override void EndVideo()
    {
        loadingScreen.gameObject.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }
}
