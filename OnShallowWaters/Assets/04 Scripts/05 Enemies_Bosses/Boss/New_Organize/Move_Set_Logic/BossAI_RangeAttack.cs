using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Range Attack")]
public class BossAI_RangeAttack : State
{
    public int maxShootCount;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.speed = 0;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;

        sm.RotateTowards();

        if (sm.inStateTimer > sm.shootInterval)
        {
            sm.shootCount += 1;
            sm.inStateTimer = 0;
            sm.ShootProjectile(sm.shootPrefab[0], sm.aimDirection[0]);
        }
        else if (sm.shootCount >= maxShootCount)
        {
            sm.shootCount = 0;
            sm.inStateTimer = 0;
            sm.Agent.speed = sm.speed;
            sm.BossRandomState();
        }
    }
}
