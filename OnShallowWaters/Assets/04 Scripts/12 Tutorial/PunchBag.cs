using UnityEngine;
using System;

public class PunchBag : MonoBehaviour, IDamageable{
    public static event Action beenAttacked;
    public int numBeenAttacked;

    public void Damage(int damageAmount){
        if(beenAttacked != null && numBeenAttacked != 3){
            beenAttacked();
            numBeenAttacked++;
        }
    }

    public float LostHP(){
        throw new System.NotImplementedException();
    }
}
