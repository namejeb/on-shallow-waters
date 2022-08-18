using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialTracker : MonoBehaviour
{
    private TutorialManager _tm;
    public bool actionOn;

    // Description
    [SerializeField] private Transform[] descriptionTexts;
    public int _descCounter = 0;
    public int actionStage = 0;

    // Action
    public static event Action changePunchBagMode;
    public int numAttack = 0;
    public int numSlash = 0;
    public int numSlam = 0;
    
    public int numDash = 0;

    public TextMeshProUGUI attackCount;
    public TextMeshProUGUI heavySlashCount;
    public TextMeshProUGUI heavySlamCount;
    
    public TextMeshProUGUI dashCount;

    [Space][Space]
    [Header("Location To Move Towards:")]
    [SerializeField] private Transform destinationVFX;
    [SerializeField] private DummyStatsWithHp dummyStatsWithHp;
    
    [Space][Space]
    [Header("Breakable Props: ")] 
    [SerializeField] private Transform breakablePropsContainer;

    

    
    void Start()
    {
        _tm = GetComponent<TutorialManager>();
        
        actionOn = false;

        DummyStatsWithHp.OnAttacked += Attack;
        DashNAttack.OnHeavySlash += HeavySlash;
        DashNAttack.OnHeavySlam += HeavySlam;
        
        Destination.OnMove += NextAction; 
        IntroManager.SwitchStage += ActionActivated;

        ExitRoomTrigger_Tutorial.OnExitTutorial += DisableLastObject;
        BreakableProps_Tutorial.OnAllPropsBroken += NextAction;
        
        dummyStatsWithHp.SetHealth(10000);
        breakablePropsContainer.gameObject.SetActive( false );
    }

    public void Move(){
        if (!actionOn) return;
        if (actionStage != 0) return;

        ActionActivated();
    }

    public void Attack(){
        if (!actionOn) return;
        if (actionStage != 1) return;

        numAttack++;
        attackCount.text = $"{numAttack} / 3";
        if (numAttack == 3){
            NextAction();
        }
    }

    public void HeavySlash()
    {
        if (!actionOn) return;
        if (actionStage != 2) return;

        numSlash++;
        heavySlashCount.text = $"{numSlash} / 3";
        if (numSlash == 3)
        {
            NextAction();
        }
    }
    
    public void HeavySlam()
    {
        if (!actionOn) return;
        if (actionStage != 3) return;

        numSlam++;
        heavySlamCount.text = $"{numSlam} / 3";
        if (numSlam == 3)
        {
            NextAction();
        }
    }

    public void Dash(){
        if (!actionOn) return;
        if (actionStage != 4) return;

        numDash++;
        dashCount.text =  $"{numDash} / 3";
        if (numDash == 3){
            NextAction();
            breakablePropsContainer.gameObject.SetActive( true );
        }
    }


    public void Kill(){
        if (!actionOn) return;
        if (actionStage != 5) return;
     
      //  NextAction();
    }

    public void Exit(){
        if (!actionOn) return;
        if (actionStage != 6) return;
        NextAction();
    }

    void NextAction(){

        if (_descCounter == 0)
        {
            destinationVFX.gameObject.SetActive(false);
        }
        
        _descCounter++;
        actionStage++;
        
        if (_descCounter == 2)
        {
            DashNAttack.OnDash += Dash;
        }

        if (_descCounter == 6)
        {
            DummyStatsWithHp.OnDeath += NextAction;
            dummyStatsWithHp.SetHealth(320);
        }


        if(_descCounter == descriptionTexts.Length){
            SetObjectActive(descriptionTexts[_descCounter - 1], false);
            GetComponent<Image>().enabled = false;
        } else {
            if(_descCounter <= descriptionTexts.Length)
                SetObjectActive(descriptionTexts[_descCounter], true);
            if(_descCounter - 1 >= 0){
                SetObjectActive(descriptionTexts[_descCounter - 1], false);
            }
        }
    }

    void ActionActivated(){
        InIt();
        actionOn = true;
        SetObjectActive(descriptionTexts[0], true);

        destinationVFX.transform.gameObject.SetActive(true);
        _tm.currentStage = TutorialManager.tutorialStage.Action;
    }

    void InIt(){
        for(int i = 0; i < descriptionTexts.Length; i++){
            SetObjectActive(descriptionTexts[i], false);
        }
        attackCount.text = $"{numAttack} / 3";
        heavySlashCount.text = $"{numSlash} / 3";
        heavySlamCount.text = $"{numSlam} / 3";
    }

    void SetObjectActive(Transform transformUI, bool status){
        transformUI.gameObject.SetActive(status);
    }

    void OnDestroy(){
        actionOn = false;
        
        DummyStatsWithHp.OnAttacked -= Attack;
        DashNAttack.OnHeavySlash -= HeavySlash;
        DashNAttack.OnHeavySlam -= HeavySlam;
        
        DummyStatsWithHp.OnDeath -= NextAction;
        DashNAttack.OnDash -= Dash;
        Destination.OnMove -= NextAction; 
        IntroManager.SwitchStage -= ActionActivated;
        ExitRoomTrigger_Tutorial.OnExitTutorial -= DisableLastObject;
        
        BreakableProps_Tutorial.OnAllPropsBroken -= NextAction;
    }

    private void DisableLastObject()
    {
        for(int i = 0; i < descriptionTexts.Length; i++){
            SetObjectActive(descriptionTexts[i], false);
        }
    }
}
