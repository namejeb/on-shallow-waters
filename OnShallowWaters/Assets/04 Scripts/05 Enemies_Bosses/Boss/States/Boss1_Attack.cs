using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Attack : Boss_AttackState
{
    public override void EnterState(Boss_FSM boss)
    {
        Debug.Log("Boss1 Attack State");
        Vector3 boxPos = boss.transform.position + boss.offset;
        Collider[] hitColliders = Physics.OverlapBox(boxPos, boss.hitboxSize / 2, Quaternion.identity, boss.enemyLayer);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Player"))
                Debug.Log("Hit Player");
        }
    }

    public override void Update(Boss_FSM boss)
    {
        
    }
}
