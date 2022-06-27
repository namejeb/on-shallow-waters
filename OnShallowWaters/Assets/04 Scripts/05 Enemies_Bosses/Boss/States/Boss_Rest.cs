using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_Rest : Boss_BaseState
{
    int choice;

    public override void EnterState(Boss_FSM boss)
    {
        //Debug.Log("Boss1_Rest");
        boss.Agent.speed = 0;
        choice = boss.RandomChoice();
        Debug.Log(choice);
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.restTimeout)
        {
            boss.inStateTimer = 0;
            boss.Agent.speed = boss.speed;
            boss.BossRandomState();
        }
        
        if (choice == 0)
            RotateTowards(boss.Target, boss);
        else if (choice == 1)
        {
            boss.Agent.speed = 2;
            boss.Agent.SetDestination(boss.Target.position);
            
        }
    }
    
    private void RotateTowards(Transform target, Boss_FSM boss)
    {
        Vector3 direction = (target.position - boss.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, lookRotation, Time.deltaTime * boss.rotationSpeed);
    }
}
