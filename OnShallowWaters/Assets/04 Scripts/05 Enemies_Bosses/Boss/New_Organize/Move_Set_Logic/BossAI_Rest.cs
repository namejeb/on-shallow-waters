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

    private int rand;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.stoppingDistance = 6.45f;
        rand = Random.Range(0, 2);
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (sm.inStateTimer > restTimeout)
        {
            sm.inStateTimer = 0;
            sm.Agent.speed = sm.speed;
            sm.Agent.stoppingDistance = sm.chaseMinDistance;
            sm.BossRandomState();
        }
        Debug.Log(sm.Agent.velocity);
        if (usingNavmesh)
        {
            sm.Agent.speed = restMoveSpeed;
            if (sm.Agent.velocity == Vector3.zero)
            {
                sm.Anim.SetBool("isWalk", false);
            }
            else
            {
                sm.Anim.SetBool("isWalk", true);
            }
            sm.Agent.SetDestination(sm.Target.position);
            
        }
        else if (!canRotate && !usingNavmesh)
        {

            if (sm.Agent.velocity == Vector3.zero)
            {
                sm.Anim.SetBool("isWalk", false);
            }
            else
            {
                sm.Anim.SetBool("isWalk", true);
            }

            if (rand == 0)
            {
                sm.RotateTowards();
            }
            else if (rand == 1)
            {
                
                sm.Agent.speed = restMoveSpeed;
                sm.Agent.SetDestination(sm.Target.position);
            }
        }
    }
}
