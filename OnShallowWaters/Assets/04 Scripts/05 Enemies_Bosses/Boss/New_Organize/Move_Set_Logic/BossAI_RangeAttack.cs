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
    [SerializeField] private float wait;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
        sm.Anim.SetTrigger(animationTrigger);
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;

        sm.RotateTowards();

        if (sm.inStateTimer > shootInterval)
        {
            shootCount += 1;
            sm.inStateTimer = 0;
            //sm.StartCoroutine(sm.ShootProjectile(sm.shootPrefab[prefabIndex], sm.aimDirection[aimerIndex], wait));
        }
        else if (shootCount >= maxShootCount)
        {
            shootCount = 0;
            sm.inStateTimer = 0;
            sm.Agent.enabled = true;
            sm.Anim.SetTrigger("toNormal");
            sm.BossRandomState();
        }
    }
}
