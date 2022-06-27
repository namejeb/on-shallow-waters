using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Stunt : Boss_BaseState
{
    public override void EnterState(Boss_FSM boss)
    {
        boss.Agent.enabled = false;
        // play stunt animation
        //Debug.Log("Boss Stunted");
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.stuntTimeout)
        {
            boss.inStateTimer = 0;
            boss.Agent.enabled = true;
            boss.BossRandomState();
        }
    }
}
