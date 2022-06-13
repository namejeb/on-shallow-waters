using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Dash : Boss_Move4
{
    private Vector3 _direction;
    
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("B1_Move_4");
        boss.Agent.enabled = false;
        _direction = (boss.Target.position - boss.transform.position).normalized;
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.dashTimeout)
        {
            boss.inStateTimer = 0;
            boss.Agent.enabled = true;
            boss.SetState(boss.restState);
        }

        boss.transform.position += _direction * boss.dashSpeed * Time.deltaTime;
    }
}
