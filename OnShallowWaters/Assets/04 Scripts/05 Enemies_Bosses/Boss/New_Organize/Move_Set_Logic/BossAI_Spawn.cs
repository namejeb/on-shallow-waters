using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Spawn")]
public class BossAI_Spawn : State
{
    [SerializeField] private string animationTrigger;
    [SerializeField] private float spawnTimeout;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
        sm.Anim.SetTrigger(animationTrigger);
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;

        if (sm.inStateTimer > spawnTimeout)
        {
            sm.inStateTimer = 0;
            sm.Anim.SetTrigger("toNormal");
            sm.Agent.enabled = true;
            sm.BossRandomState();
        }

        //if (sm.inStateTimer < spawnTimeout - 3)
        //    sm.RotateTowards();
    }
}
