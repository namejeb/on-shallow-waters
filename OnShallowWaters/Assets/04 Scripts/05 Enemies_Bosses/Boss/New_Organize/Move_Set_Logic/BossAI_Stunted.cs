using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Stunted")]
public class BossAI_Stunted : State
{
    [SerializeField] private float stuntTimeout;
    [SerializeField] private string stuntAnimation;

    public override void EnterState(StateMachineManager sm)
    {
        sm.Agent.enabled = false;
        sm.Anim.SetTrigger(stuntAnimation);
    }

    public override void UpdateState(StateMachineManager sm)
    {
        sm.inStateTimer += Time.deltaTime;
        if (sm.inStateTimer > stuntTimeout)
        {
            sm.inStateTimer = 0;
            sm.Agent.enabled = true;
            sm.BossRandomState();
        }
    }
}
