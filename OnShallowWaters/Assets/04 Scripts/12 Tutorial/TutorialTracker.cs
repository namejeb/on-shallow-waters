using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VLB;

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
    public int numDash = 0;

    public TextMeshProUGUI attackCount;
    public TextMeshProUGUI dashCount;

    [Header("Location To Move Towards:")]
    [SerializeField] private Transform destinationVFX;

    [SerializeField] private DummyStatsWithHp dummyStatsWithHp;
    
    

    void Start()
    {
        _tm = GetComponent<TutorialManager>();
        
        actionOn = false;
        //DashNAttack.OnAttack += Attack;
        DummyStatsWithHp.OnAttacked += Attack;
        
        Destination.OnMove += NextAction; 
        IntroManager.SwitchStage += ActionActivated;
        
        dummyStatsWithHp.SetHealth(10000);
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
        attackCount.text = numAttack + " / 3";
        if (numAttack == 3){
            NextAction();
        }
    }

    public void Dash(){
        if (!actionOn) return;
        if (actionStage != 2) return;

        numDash++;
        dashCount.text = numDash + " / 3";
        if (numDash == 3){
            NextAction();
        }
    }

    public void Kill(){
        if (!actionOn) return;
        if (actionStage != 3) return;
     
      //  NextAction();
    }

    public void Exit(){
        if (!actionOn) return;
        if (actionStage != 4) return;
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

        if (_descCounter == 3)
        {
            DummyStatsWithHp.OnDeath += NextAction;
            dummyStatsWithHp.SetHealth(380);
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
    }

    void SetObjectActive(Transform transformUI, bool status){
        transformUI.gameObject.SetActive(status);
    }

    void OnDestroy(){
        actionOn = false;
      //  DashNAttack.OnAttack -= Attack;
        DummyStatsWithHp.OnAttacked -= Attack;
        DummyStatsWithHp.OnDeath -= NextAction;
        DashNAttack.OnDash -= Dash;
        Destination.OnMove -= NextAction; 
    }
}
