using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Attack : Boss_AttackState
{
    private static readonly int MeleeAttack1 = Animator.StringToHash("MeleeAttack1");

    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("Boss1 Attack State");
        boss.Anim.SetTrigger(MeleeAttack1);
    }

    public override void Update(Boss_FSM boss)
    {
        if (boss.attacked)
        {
            boss.attacked = false;
            boss.SetState(boss.restState);
        }
    }
}
