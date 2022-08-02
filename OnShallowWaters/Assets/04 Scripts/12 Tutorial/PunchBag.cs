using UnityEngine;
using System;

public class PunchBag : MonoBehaviour, IDamageable{
    public static event Action beenAttacked;
    public static event Action beenKilled;
    public int numBeenAttacked;

    public void Damage(int damageAmount){
        if(beenAttacked != null){
            beenAttacked();
        }
    }

    public float GetReceivedDamage(float outDamage)
    {
        return outDamage;
    }

    public float LostHP(){
        throw new System.NotImplementedException();
    }
}
