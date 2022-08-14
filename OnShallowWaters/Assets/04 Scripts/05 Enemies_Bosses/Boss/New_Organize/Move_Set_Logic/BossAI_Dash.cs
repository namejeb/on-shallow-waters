using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Dash")]
public class BossAI_Dash : State
{
    [SerializeField] private string chargeAnimation;
    [SerializeField] private string dashAnimation;
    [SerializeField] private float rotateTime;
    [SerializeField] private float dashStartTime;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTimeout;
    public bool isDashing;

    private Vector3 _direction;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
        sm.Anim.SetTrigger(chargeAnimation);
    }

    public override void UpdateState(StateMachineManager sm)
    {
        
        sm.inStateTimer += Time.deltaTime;

        if (sm.inStateTimer < rotateTime)
        {
            sm.RotateTowards();

            _direction = (sm.Target.position - sm.transform.position).normalized;
            Debug.Log(sm.transform.eulerAngles + ", " + sm.transform.rotation + ", " + _direction);
        }

        if (sm.inStateTimer > dashStartTime && sm.inStateTimer <= dashTimeout)
        {
            sm.Anim.SetTrigger(dashAnimation);
            sm.Rb.isKinematic = false;
            sm.Rb.AddForce(new Vector3(_direction.x, 0, _direction.z) * dashForce);
        }

        if (sm.inStateTimer > dashTimeout)
        {
            isDashing = false;
            sm.Anim.ResetTrigger("isDash");
            sm.Anim.SetTrigger("toNormal");
            sm.inStateTimer = 0;
            sm.HitBoxOff(2);
            sm.Rb.velocity = Vector3.zero;
            sm.Rb.isKinematic = true;
            sm.Agent.enabled = true;
            sm.Agent.ResetPath();
            sm.BossRandomState();
        }
    }
}
