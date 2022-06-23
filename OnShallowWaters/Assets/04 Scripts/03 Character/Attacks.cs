using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] private float lightAttack;
    [SerializeField] private float chargeAttack;
    [SerializeField] private float nextAttack;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private int attackSequence;

    [SerializeField] private bool isAttack = false;
    
    [SerializeField] private Animator animator;

    void Start()
    {
        attackSequence = 0;
    }

    
    void Update()
    {
        
       
    }

    public void Attack()
    {
        if (attackSequence == 0 && Time.time > nextAttack)
        {
            playerMovement.enabled = false;
            animator.SetTrigger("Attack");
            attackSequence++;
            nextAttack = Time.time + 1;
        }
        else if(attackSequence == 1 && Time.time > nextAttack)
        {
            playerMovement.enabled = false;
            animator.SetTrigger("Attack");
            attackSequence++;
            nextAttack = Time.time + 1;
        }
        else if(attackSequence == 1 && Time.time > nextAttack)
        {
            playerMovement.enabled = false;
            animator.SetTrigger("Attack");
            attackSequence = 0;
            nextAttack = Time.time + 1.5f;
            
        }
        
    }
}
