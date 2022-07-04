using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss_AI/Die")]
public class BossAI_Die : State
{
    public override void EnterState(StateMachineManager sm)
    {
        //play animation
        Debug.Log("Die State");
        //boss.Agent.ResetPath();
        //boss.Agent.isStopped = true;
        sm.gameObject.SetActive(false);
    }

    public override void UpdateState(StateMachineManager sm)
    {

    }
}
