using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_SlamGround : Boss_Move3
{
    public override void EnterState(Boss_FSM boss)
    {
        //Debug.Log("B1_Move_3");
        boss.Agent.speed = 0;
    }

    public override void Update(Boss_FSM boss)
    {
        boss.inStateTimer += Time.deltaTime;
        if (boss.inStateTimer > boss.shootInterval)
        {
            boss.shootCount += 1;
            boss.inStateTimer = 0;
            boss.ShootProjectile2(boss.shootPrefab[1], boss.aimDirection[1]);
        }
        else if (boss.shootCount >= 1)
        {
            boss.shootCount = 0;
            boss.inStateTimer = 0;
            boss.Agent.speed = boss.speed;
            boss.SetState(boss.restState);
        }
        
        if (boss.inStateTimer > boss.shootInterval - 2)
            RotateTowards(boss.Target, boss);
    }
    
    private void RotateTowards(Transform target, Boss_FSM boss)
    {
        Vector3 direction = (target.position - boss.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, lookRotation, Time.deltaTime * boss.rotationSpeed);
    }
}
