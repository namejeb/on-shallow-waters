using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_Rest : Boss_BaseState
{
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("Boss1 Rest State");
        boss.Agent.speed = 0;
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.restTimeout)
        {
            boss.inStateTimer = 0;
            boss.Agent.speed = boss.speed;
            boss.SetState(boss.move4State);
        }

        RotateTowards(boss.Target, boss);
    }
    
    private void RotateTowards(Transform target, Boss_FSM boss)
    {
        Vector3 direction = (target.position - boss.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, lookRotation, Time.deltaTime * boss.rotationSpeed);
    }
}
