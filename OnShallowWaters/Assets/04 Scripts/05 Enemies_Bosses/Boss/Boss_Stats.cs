using System;
using System.Collections;
using System.Collections.Generic;
using _04_Scripts._05_Enemies_Bosses.Enemy;
using UnityEngine;

public class Boss_Stats : CharacterStats, IDamageable
{
    public int maxArmour;
    public int currArmour;
    public bool armState;

    private Boss_FSM _bossFsm;

    private new void Awake()
    {
        _bossFsm = GetComponent<Boss_FSM>();
        base.Awake();
    }
    
    private void Start()
    {
        currArmour = maxArmour;
        print($"{currHp}/{MaxHp}");
    }

    public void Damage(int damageAmount)
    {
        if (armState)
        {
            if (currArmour > 0)
            {
                currArmour -= damageAmount;    
            }
            else
            {
                currArmour = 0;
                armState = false;
                _bossFsm.SetState(_bossFsm.stuntState);
            }
        }
        else
        {
            TakeDamage(damageAmount);
        }
    }
}
