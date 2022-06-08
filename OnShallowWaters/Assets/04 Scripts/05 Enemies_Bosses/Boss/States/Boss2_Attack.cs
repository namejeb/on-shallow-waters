using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Attack : Boss_AttackState
{
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("Boss2 Attack State");
    }

    public override void Update(Boss_FSM boss)
    {

    }
}
