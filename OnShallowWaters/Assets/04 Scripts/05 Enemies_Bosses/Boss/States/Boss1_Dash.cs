using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Dash : Boss_Move4
{
    private Vector3 _direction;
    private Boss1_Config _b1Config;
    
    public override void EnterState(Boss_FSM boss)
    {
        //Debug.Log("B1_Move_4");
        boss.Agent.enabled = false;
        _direction = (boss.Target.position - boss.transform.position).normalized;
        _b1Config = boss.gameObject.GetComponent<Boss1_Config>();
        _b1Config.dashSpeed = boss.value[0];
        _b1Config.dashTimeout = boss.value[1];
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.value[1])
        {
            boss.inStateTimer = 0;
            boss.Agent.enabled = true;
            boss.SetState(boss.restState);
        }

        //boss.transform.position += _direction * boss.value[0] * Time.deltaTime;
        boss.Rb.MovePosition(boss.transform.position + _direction * Time.deltaTime * boss.value[0]);
    }
}
