using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Spawn")]
public class BossAI_Spawn : State
{
    [SerializeField] private int prefabIndex;
    [SerializeField] private int aimerIndex;
    [SerializeField] private int spawnCount;
    [SerializeField] private int spawnMax;
    [SerializeField] private float spawnInterval;
    

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (spawnCount < spawnMax && sm.inStateTimer > spawnInterval)
        {
            spawnCount += 1;
            sm.inStateTimer = 0;
            sm.ShootProjectile2(sm.shootPrefab[prefabIndex], sm.aimDirection[aimerIndex]);
        }
        
        if (spawnCount >= spawnMax && sm.inStateTimer > spawnInterval)
        {
            spawnCount = 0;
            sm.Agent.enabled = true;
            sm.BossRandomState();
        }

        //if (sm.inStateTimer > spawnInterval - 2)
        //    sm.RotateTowards();
    }
}
