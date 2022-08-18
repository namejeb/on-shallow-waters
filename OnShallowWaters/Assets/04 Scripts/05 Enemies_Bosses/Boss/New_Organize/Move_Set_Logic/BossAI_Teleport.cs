using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Teleport")]
public class BossAI_Teleport : State
{
    public override void EnterState(StateMachineManager sm)
    {
        if (sm.Stats.armState)
        {
            sm.BossRandomState();
        }
        else
        {
            sm.Agent.ResetPath();
            sm.StartCoroutine(sm.Dissolving());
        }
    }

    public override void UpdateState(StateMachineManager sm)
    {
        
    }
}
