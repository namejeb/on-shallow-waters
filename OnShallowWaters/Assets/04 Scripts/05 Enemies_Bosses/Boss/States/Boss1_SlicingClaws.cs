using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_SlicingClaws : Boss_Move1
{
    private static readonly int MeleeAttack1 = Animator.StringToHash("MeleeAttack1");

    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("B1_Move_1");
        boss.Agent.stoppingDistance = boss.chaseMinDistance; 
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.chaseTimeout)
        {
            boss.inStateTimer = 0;
            boss.SetState(boss.restState);
        }
        
        if (boss.Agent.speed != 0)
            boss.Agent.SetDestination(boss.Target.position);

        if (Vector3.Distance(boss.transform.position, boss.Target.position) < (boss.Agent.stoppingDistance + boss.attackDistOffset) && !boss.isAttacking)
        {
            boss.isAttacking = true;
            boss.Agent.speed = 0;
            boss.Anim.SetTrigger(MeleeAttack1);
        }

        if (boss.isAttackFin)
        {
            boss.isAttacking = false;
            boss.isAttackFin = false;
            boss.Agent.speed = boss.speed;
            boss.inStateTimer = 0;
            boss.SetState(boss.restState);
        }
    }
}
