using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AttackState : Boss_BaseState
{
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("Base Attack State");
    }

    public override void Update(Boss_FSM boss)
    {
        
    }
}
