using System;
using UnityEngine;

public class DummyStats : CharacterStats, IDamageable
{
    // ref
    private Animator _anim;
    
    [SerializeField] private Stat defense;
    private float _defPercent = 1f;


    private new void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private float ReceiveIncomingDamage(float incomingDamage)
    {
        incomingDamage *= (100 / (25 + (_defPercent * defense.CurrentValue)));
        return incomingDamage;
    }

    public void Damage(int damageAmount)
    {
        _anim.SetTrigger("HitTrigger");
    }

    public float GetReceivedDamage(float outDamage)
    {
        return ReceiveIncomingDamage(outDamage);
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
}
