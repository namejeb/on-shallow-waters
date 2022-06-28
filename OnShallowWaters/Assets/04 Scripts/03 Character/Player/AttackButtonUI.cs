using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool isSlash;
    public float chargedTimer;
    public bool isSlam;
    public bool isPressed;
    public DashNAttack DNA;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        if (isPressed)
        {
            DNA.Attack();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
       
        isPressed = false;
        if (chargedTimer >= 1 && chargedTimer <2)
        {
            isSlash=true;
            Debug.Log("KAHHHHHHHBIIIIIN");
        }
        
        else if (chargedTimer >= 2)
        {
            isSlam=true;
            Debug.Log("BOMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
        }
         chargedTimer = 0;
    }


   
}
