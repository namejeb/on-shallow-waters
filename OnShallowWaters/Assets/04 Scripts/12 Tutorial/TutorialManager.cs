using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Transform kiddTransform;
    
    public enum tutorialStage{
        Intro, Action, End
    } 
    public tutorialStage currentStage;

    void Start(){
        currentStage = tutorialStage.Intro;

        kiddTransform.GetComponent<PlayerMovement>().enabled = false;
        kiddTransform.GetComponent<DashNAttack>().enabled = false;
        
        Invoke(nameof(EnableMovement), 2f);
    }

    private void EnableMovement()
    {
        kiddTransform.GetComponent<PlayerMovement>().enabled = true;
        kiddTransform.GetComponent<DashNAttack>().enabled = true;
    }
}
