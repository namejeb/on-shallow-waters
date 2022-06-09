using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_Rest : Boss_BaseState
{
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("Boss1 Rest State");
        boss.Agent.isStopped = true;
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.restTimeout)
        {
            boss.inStateTimer = 0;
            boss.Agent.isStopped = false;
            boss.SetState(boss.chaseState);
        }
    }
}
