using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialTracker : MonoBehaviour
{
    public bool actionOn;

    [SerializeField] private Transform[] descriptionTexts;
    public int _descCounter = 0;
    public int actionStage = 0;

    public static event Action changePunchBagMode;
    public int numAttack = 0;
    public int numDash = 0;

    public TextMeshProUGUI attackCount;
    public TextMeshProUGUI dashCount;

    void Start(){
        actionOn = false;
        DashNAttack.OnAttack += Attack;
        DashNAttack.OnDash += Dash;
        Destination.OnMove += Move; 
        IntroManager.SwitchStage += ActionActivated;
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
        if (numAttack == 3){
            attackCount.text = numAttack + " / 3";
            ActionActivated();
        }
    }

    public void Dash(){
        if (!actionOn) return;
        if (actionStage != 2) return;

        numDash++;
        if (numDash == 3){
            dashCount.text = numDash + " / 3";
            ActionActivated();
        }
    }

    public void Kill(){
        if (!actionOn) return;
        if (actionStage != 3) return;
        ActionActivated();
    }

    public void Exit(){
        if (!actionOn) return;
        if (actionStage != 4) return;
        ActionActivated();
    }

    void NextAction(){
        _descCounter++;
        actionStage++;

        if(_descCounter == descriptionTexts.Length){
            SetObjectActive(descriptionTexts[_descCounter - 1], false);
            GetComponent<Image>().enabled = false;
        } else {
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
        DashNAttack.OnAttack -= Attack;
        DashNAttack.OnDash -= Dash;
        Destination.OnMove -= Move; 
    }
}
