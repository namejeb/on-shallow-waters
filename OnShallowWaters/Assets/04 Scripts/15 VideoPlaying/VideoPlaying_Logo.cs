using UnityEngine.SceneManagement;

public class VideoPlaying_Logo : VideoPlaying
{
    protected override void EndVideo()
    {
        SceneManager.LoadSceneAsync((int) SceneData.MainMenu);
    }
}
