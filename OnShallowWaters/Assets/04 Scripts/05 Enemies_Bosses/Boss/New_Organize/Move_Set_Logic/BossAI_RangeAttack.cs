using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Range Attack")]
public class BossAI_RangeAttack : State
{
	[SerializeField] private string animationTrigger;
    [SerializeField] private int prefabIndex;
    [SerializeField] private int aimerIndex;
    [SerializeField] private int shootCount;
    [SerializeField] private int maxShootCount;
    [SerializeField] private float shootInterval;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.speed = 0;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;

        sm.RotateTowards();

        if (sm.inStateTimer > shootInterval)
        {
            shootCount += 1;
            sm.inStateTimer = 0;
            sm.ShootProjectile(sm.shootPrefab[prefabIndex], sm.aimDirection[aimerIndex]);
        }
        else if (shootCount >= maxShootCount)
        {
            shootCount = 0;
            sm.inStateTimer = 0;
            sm.Agent.speed = sm.speed;
            sm.BossRandomState();
        }
    }
}
