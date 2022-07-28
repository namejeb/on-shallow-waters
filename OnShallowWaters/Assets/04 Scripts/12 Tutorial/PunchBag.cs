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

    public float LostHP(){
        throw new System.NotImplementedException();
    }
}
