using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Rest")]
public class BossAI_Rest : State
{
    [Header("Default")]
    [SerializeField] private float restTimeout;

    [Header("Rotate")]
    [SerializeField] private bool canRotate;

    [Header("Move(Navmesh)")]
    [SerializeField] private bool usingNavmesh;
    [SerializeField] private float restMoveSpeed;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.speed = 0;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (sm.inStateTimer > restTimeout)
        {
            sm.inStateTimer = 0;
            sm.Agent.speed = sm.speed;
            sm.BossRandomState();
        }

        if (canRotate)
            sm.RotateTowards();
        else if (usingNavmesh)
        {
            sm.Agent.speed = restMoveSpeed;
            sm.Agent.SetDestination(sm.Target.position);
        }
        else if (!canRotate && !usingNavmesh)
        {
            float rand = Random.Range(0, 2);
            if (rand == 0)
                sm.RotateTowards();
            else if (rand == 1)
            {
                sm.Agent.speed = restMoveSpeed;
                sm.Agent.SetDestination(sm.Target.position);
            }
        }
    }
}
