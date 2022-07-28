using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public enum tutorialStage{
        Intro, Action, End
    } 
    public tutorialStage currentStage;

    void Start(){
        currentStage = tutorialStage.Intro;
    }
}
