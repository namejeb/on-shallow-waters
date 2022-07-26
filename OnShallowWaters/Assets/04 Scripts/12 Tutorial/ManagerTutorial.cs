using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTutorial : MonoBehaviour
{
    public enum tutorialStage{
        Intro, Action, End
    } 
    public tutorialStage currentStage;

    void Start(){
        currentStage = tutorialStage.Intro;
    }

    void Update(){
        
    }


}
