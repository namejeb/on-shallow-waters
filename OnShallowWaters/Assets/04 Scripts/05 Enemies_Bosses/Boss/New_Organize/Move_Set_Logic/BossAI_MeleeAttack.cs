using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Melee Attack")]
public class BossAI_MeleeAttack : State
{
    public string animationTrigger;

    public override void EnterState(StateMachineManager sm)
    {

    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (sm.inStateTimer > sm.chaseTimeout)
        {
            sm.inStateTimer = 0;
            sm.SetState(sm.stateList[0]);
        }

        //Debug.Log(Vector3.Distance(sm.transform.position, sm.Target.position));

        if (sm.Agent.speed != 0)
            sm.Agent.SetDestination(sm.Target.position);

        if (Vector3.Distance(sm.transform.position, sm.Target.position) < (sm.Agent.stoppingDistance + sm.attackDistOffset) && !sm.isAttacking)
        {
            sm.isAttacking = true;
            sm.Agent.speed = 0;
            sm.Anim.SetTrigger(animationTrigger);
        }

        if (sm.isAttackFin)
        {
            sm.isAttacking = false;
            sm.isAttackFin = false;
            sm.Agent.speed = sm.speed;
            sm.inStateTimer = 0;
            sm.BossRandomState();
        }
    }
}
