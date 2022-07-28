using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _04_Scripts._05_Enemies_Bosses;

public class AttackHitbox : MonoBehaviour
{
    public DashNAttack damage;
    public SkBlessing _skBlessing;

    private void OnTriggerEnter(Collider other)
    {
        float tempOutDamage = (float)damage.outDamage;
        IDamageable damagable = other.GetComponent<IDamageable>();

        EnemyHandler enemyhandler = null;

        if (other.tag == "Enemy")
        {
            enemyhandler = other.GetComponent<EnemyHandler>();

            if(enemyhandler != null)
            {
                Debug.Log(other.gameObject.name);
                tempOutDamage = damage.HandleBoonDmgModifications(tempOutDamage, enemyhandler);
                Debug.Log(tempOutDamage);
            }
            _skBlessing.AddSoul(2);
        }

        damagable.Damage((int)tempOutDamage);
        

    }
}
