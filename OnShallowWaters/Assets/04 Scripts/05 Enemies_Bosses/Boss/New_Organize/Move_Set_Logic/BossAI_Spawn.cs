using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Spawn")]
public class BossAI_Spawn : State
{
    [SerializeField] private string animationTrigger;
    [SerializeField] private int prefabIndex;
    [SerializeField] private int aimerIndex;
    [SerializeField] private int spawnCount;
    [SerializeField] private int spawnMax;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float waitAnimationTime;
    

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
            sm.Anim.SetTrigger(animationTrigger);
        }

        if (spawnCount >= spawnMax && sm.inStateTimer > spawnInterval)
        {
            sm.Anim.SetTrigger("toNormal");
            spawnCount = 0;
            sm.Agent.enabled = true;
            sm.BossRandomState();
        }

        if (sm.inStateTimer > spawnInterval - 2)
            sm.RotateTowards();
    }
}
