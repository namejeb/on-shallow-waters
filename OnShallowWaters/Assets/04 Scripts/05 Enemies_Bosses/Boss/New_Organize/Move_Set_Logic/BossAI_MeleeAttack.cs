using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Melee Attack")]
public class BossAI_MeleeAttack : State
{
    [SerializeField] private string animationTrigger;
    [SerializeField] private float chaseTimeout;
    [SerializeField] private float attackDistOffset;

    public override void EnterState(StateMachineManager sm)
    {

    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (sm.inStateTimer > chaseTimeout)
        {
            sm.inStateTimer = 0;
            sm.SetState(sm.stateList[0]);
        }

        //Debug.Log(Vector3.Distance(sm.transform.position, sm.Target.position));

        if (sm.Agent.speed != 0)
            sm.Agent.SetDestination(sm.Target.position);

        if (Vector3.Distance(sm.transform.position, sm.Target.position) < (sm.chaseMinDistance + attackDistOffset) && !sm.isAttacking)
        {
            sm.isAttacking = true;
            sm.Agent.speed = 0;
            sm.Agent.enabled = false;
            sm.Anim.SetTrigger(animationTrigger);
        }

        if (sm.isAttackFin)
        {
            sm.isAttacking = false;
            sm.isAttackFin = false;
            sm.Agent.enabled = true;
            sm.Agent.ResetPath();
            sm.Agent.speed = sm.speed;
            sm.inStateTimer = 0;
            sm.BossRandomState();
        }
    }
}
