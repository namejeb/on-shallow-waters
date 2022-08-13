using _04_Scripts._01_Event_System.Start_Pause;
using UnityEngine;

public class VideoPlaying_Cutscene : VideoPlaying
{
    [SerializeField] private MainMenu mainMenu;
    

    private void Start()
    {
        GameManager.SetIsFirstPlayThrough(false);
    }

    protected override void EndVideo()
    {
        mainMenu.Tutorial();
    }

    // called in Skip button
    public void End()
    {
        EndVideo();
    }
}
