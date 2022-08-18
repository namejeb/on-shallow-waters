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
        rand = 3;// Random.Range(0, 3);
        if (rand == 3)
        {
            int point = Random.Range(0, sm.teleportPoints.Count);
            Vector3 pos = new(sm.teleportPoints[point].position.x, sm.transform.position.y, sm.teleportPoints[point].position.z);
            sm.Agent.SetDestination(pos);
        }
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

        if (sm.Agent.velocity == Vector3.zero)
        {
            sm.Anim.SetBool("isWalk", false);
        }
        else
        {
            sm.Anim.SetBool("isWalk", true);
        }

        #region No Needed, But leave here
        //if (canRotate)
        //{
        //    sm.RotateTowards();
        //}
        //else if (usingNavmesh)
        //{
        //    sm.Agent.speed = restMoveSpeed;
        //    if (sm.Agent.velocity == Vector3.zero)
        //    {
        //        sm.Anim.SetBool("isWalk", false);
        //    }
        //    else
        //    {
        //        sm.Anim.SetBool("isWalk", true);
        //    }

        //    if (sm.Agent.enabled)
        //        sm.Agent.SetDestination(sm.Target.position);

        //}
        //else if (!canRotate && !usingNavmesh)
        //{

        //    if (sm.Agent.velocity == Vector3.zero)
        //    {
        //        sm.Anim.SetBool("isWalk", false);
        //    }
        //    else
        //    {
        //        sm.Anim.SetBool("isWalk", true);
        //    }

        //    if (rand == 0)
        //    {
        //        sm.RotateTowards();
        //    }
        //    else if (rand == 1)
        //    {
        //        if (sm.Agent.enabled)
        //        {
        //            sm.Agent.speed = restMoveSpeed;
        //            sm.Agent.SetDestination(sm.Target.position);
        //        }  
        //    }
        //}
        #endregion
    }
}
