using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Die")]
public class BossAI_Die : State
{
    public string deathAnimation;

    public override void EnterState(StateMachineManager sm)
    {
        //play animation
        sm.Anim.SetTrigger(deathAnimation);
    }

    public override void UpdateState(StateMachineManager sm)
    {

    }
}
