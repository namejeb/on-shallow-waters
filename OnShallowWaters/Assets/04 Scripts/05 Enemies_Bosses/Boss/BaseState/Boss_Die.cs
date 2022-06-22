using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Die : Boss_BaseState
{
    public override void EnterState(Boss_FSM boss)
    {
        //play animation
        Debug.Log("Die State");
        boss.Agent.enabled = false;
    }

    public override void Update(Boss_FSM boss)
    {
        
    }
}
