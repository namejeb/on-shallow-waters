using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Nothing")]
public class BossAI_Nothing : State
{
    public override void EnterState(StateMachineManager sm)
    {
        
    }

    public override void UpdateState(StateMachineManager sm)
    {
        if (sm.startBattle)
        {
            sm.SetState(sm.stateList[0]);
        }
    }
}
