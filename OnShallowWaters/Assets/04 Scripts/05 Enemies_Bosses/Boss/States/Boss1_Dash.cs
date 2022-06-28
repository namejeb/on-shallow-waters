using UnityEngine;

public class Boss1_Dash : Boss_Move4
{
    private Vector3 _direction;
    private Boss1_Config _b1Config;
    
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("B1_Move_4");

        //boss.Agent.ResetPath();
        boss.Agent.enabled = false;
        boss.HitBoxOn(1);
        _b1Config = boss.gameObject.GetComponent<Boss1_Config>();
        boss.value[0] = _b1Config.dashSpeed;
        boss.value[1] = _b1Config.dashTimeout;
        boss.Agent.speed = _b1Config.dashSpeed;
        _direction = (boss.Target.position - boss.transform.position);
        //boss.Agent.SetDestination(boss.Target.position * 1.5f); 
    }
    
    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;

        if (boss.inStateTimer > 1.5)
        {
            boss.RotateTowards(boss.Target, boss);
        }

        if (boss.inStateTimer > boss.value[1])
        {
            Debug.Log("Change");
            boss.inStateTimer = 0;
            boss.Agent.speed = boss.speed;
            boss.HitBoxOff(1);
            boss.Rb.velocity = Vector3.zero;
            boss.Agent.enabled = true;
            boss.Agent.ResetPath();
            boss.SetState(boss.restState);
        }
        //boss.transform.position += _direction * boss.value[0] * Time.deltaTime;
        boss.Rb.AddForce(new Vector3(_direction.x, 0, _direction.z) * 100);
        //boss.Rb.MovePosition(_direction * boss.value[0]);
        //boss.Agent.velocity = _direction * boss.value[0] * Time.deltaTime;
    }
}
