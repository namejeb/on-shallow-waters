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

    public int hhp;

    private Boss_FSM _bossFsm;

    private new void Awake()
    {
        _bossFsm = GetComponent<Boss_FSM>();
        base.Awake();
    }
    
    private void Start()
    {
        currArmour = maxArmour;
    }

    private void Update()
    {
        //just for debug purposes, will be removed
        if (hhp != currHp)
        {
            currHp = hhp;
        }

        if (currArmour <= 0 && armState)
        {
            currArmour = 0;
            armState = false;
            _bossFsm.SetState(_bossFsm.stuntState);
        }
    }

    public void Damage(int damageAmount)
    {
        if (armState)
        {
            if (currArmour > 0)
            {
                currArmour -= damageAmount;    
            }
        }
        else
        {
            TakeDamage(damageAmount);
        }
    }
}
