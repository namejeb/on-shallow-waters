using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Dash")]
public class BossAI_Dash : State
{
    [SerializeField] private float rotateTime;
    [SerializeField] private float dashStartTime;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTimeout;
    
    private Vector3 _direction;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;

        if (sm.inStateTimer < rotateTime)
        {
            sm.RotateTowards();
            _direction = (sm.Target.position - sm.transform.position);
        }

        if (sm.inStateTimer > dashTimeout)
        {
            sm.inStateTimer = 0;
            sm.HitBoxOff(1);
            sm.Rb.velocity = Vector3.zero;
            sm.Rb.isKinematic = true;
            sm.Agent.enabled = true;
            sm.Agent.ResetPath();
            sm.BossRandomState();
        }
        
        if (sm.inStateTimer > dashStartTime)
        {
            sm.HitBoxOn(1);
            sm.Rb.isKinematic = false;
            sm.Rb.AddForce(new Vector3(_direction.x, 0, _direction.z) * dashForce);
        }
    }
}
