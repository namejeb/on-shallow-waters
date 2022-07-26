using UnityEngine;
using System;

public class SwipeDash : MonoBehaviour
{

    [SerializeField] private float swipeRange;
    [SerializeField] private float tapRange;
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    private DashNAttack dna;

    float firstTapTime;
    [SerializeField]float tapAttack;
    Vector3 firstTouchPos;
    public float swipeDuration;


    private void Awake()
    {
        dna = GetComponent<DashNAttack>();
    }

    void Update()
    {
        //TAP
         if (Input.GetMouseButtonDown(0))
         {
             Debug.Log($"Tapped!");
             firstTapTime = Time.time;
             firstTouchPos = Input.mousePosition;
        
             if (firstTouchPos.x > Screen.width / 2)
             {
                 dna.Attack();
             }
         }

         //SWIPE
         if (Input.GetMouseButtonUp(0))
         {
             Vector2 swipeDelta = Input.mousePosition - firstTouchPos;
             
             if (firstTouchPos.x > Screen.width / 2)
             {
                 if (swipeDelta != Vector2.zero && Time.time - firstTapTime <= swipeDuration)
                 {
                     dna.ActivateDash();
                     animator.SetBool("Dash", false);
                 }
             }
         }
    }
}
