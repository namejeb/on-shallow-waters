using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Dash")]
public class Dash : State
{
    public float dashSpeed;
    public float dashTimeout;
    public Vector3 _direction;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
        sm.Agent.speed = dashSpeed;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;

        if (sm.inStateTimer < 3)
        {
            sm.RotateTowards();
            _direction = (sm.Target.position - sm.transform.position);
        }

        if (sm.inStateTimer > dashTimeout)
        {
            sm.inStateTimer = 0;
            sm.Agent.speed = sm.speed;
            sm.HitBoxOff(1);
            sm.Rb.velocity = Vector3.zero;
            sm.Rb.isKinematic = true;
            sm.Agent.enabled = true;
            sm.Agent.ResetPath();
            sm.SetState(sm.stateList[1]);
        }
        
        if (sm.inStateTimer > 3)
        {
            sm.HitBoxOn(1);
            sm.Rb.isKinematic = false;
            sm.Rb.AddForce(new Vector3(_direction.x, 0, _direction.z) * 100);
        }
    }
}
