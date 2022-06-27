using UnityEngine;

public class Boss1_Dash : Boss_Move4
{
    private bool dash;
    private Vector3 _direction;
    private Boss1_Config _b1Config;
    
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("B1_Move_4");

        // boss.Agent.ResetPath();
        // _direction = (boss.Target.position - boss.transform.position).normalized;
         _b1Config = boss.gameObject.GetComponent<Boss1_Config>();
        // boss.value[0] = _b1Config.dashSpeed;
         boss.value[1] = _b1Config.dashTimeout;
         boss.Agent.speed = _b1Config.dashSpeed;

        boss.Agent.SetDestination(boss.Target.position * 1.5f); 
    }
    
    public override void Update(Boss_FSM boss)
    {
        // boss.inStateTimer += Time.deltaTime;
        //
        // if (boss.inStateTimer < boss.value[1] && !dash)
        // { 
        //     boss.RotateTowards(boss.Target, boss); 
        // }
        // else if (boss.inStateTimer > boss.value[1] && !dash)
        // {
        //     dash = true;
        // }
        //
        // if (dash) 
        // {
        //     boss.Agent.Move(_direction * boss.value[0]);
        // }
        //
        if (boss.inStateTimer > boss.value[1])
        {
            boss.inStateTimer = 0;
            // boss.Agent.enabled = true;
            boss.Agent.speed = boss.speed;
            dash = false;
            boss.SetState(boss.restState);
        }
        //boss.transform.position += _direction * boss.value[0] * Time.deltaTime;
    }
}
