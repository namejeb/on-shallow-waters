using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chase : Boss_BaseState
{
    public override void EnterState(Boss_FSM boss)
    {
        boss.Agent.stoppingDistance = boss.chaseMinDistance;
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.chaseTimeout)
        {
            boss.inStateTimer = 0;
            boss.SetState(boss.restState);
            //boss.BossRandomState();
        }

        if (Vector3.Distance(boss.transform.position, boss.Target.position) <= boss.chaseMinDistance)
        {
            // Perform to attack melee state
            boss.SetState(boss.attackState);
        }

        boss.Agent.destination = new Vector3(boss.Target.position.x, boss.transform.position.y, boss.Target.position.z);
    }
}
