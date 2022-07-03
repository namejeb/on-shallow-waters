using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Spawn")]
public class BossAI_Spawn : State
{
    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.speed = 0;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (sm.inStateTimer > sm.shootInterval)
        {
            sm.shootCount += 1;
            sm.inStateTimer = 0;
            sm.ShootProjectile2(sm.shootPrefab[1], sm.aimDirection[1]);
        }
        else if (sm.shootCount >= 1)
        {
            sm.shootCount = 0;
            sm.inStateTimer = 0;
            sm.Agent.speed = sm.speed;
            sm.BossRandomState();
        }

        if (sm.inStateTimer > sm.shootInterval - 2)
            sm.RotateTowards();
    }
}
