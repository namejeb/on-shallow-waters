using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlaying_EndCredits : VideoPlaying
{
    [SerializeField] private Transform loadingScreen;

    private void Start()
    {
        loadingScreen.gameObject.SetActive( false );
    }
    protected override void EndVideo()
    {
        loadingScreen.gameObject.SetActive( true );
        Invoke( nameof(Load), 1f);
    }

    // called in Skip button
    public void End()
    {
        EndVideo();
    }

    private void Load()
    {
        SceneManager.LoadSceneAsync( (int) SceneData.MainMenu);
    }
}
