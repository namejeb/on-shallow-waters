using System;
using System.Collections;
using System.Collections.Generic;
using _04_Scripts._05_Enemies_Bosses;
using _04_Scripts._05_Enemies_Bosses.Enemy;
using UnityEngine;

public class Boss_Stats : CharacterStats, IDamageable
{
    public int maxArmour;
    public int currArmour;
    public bool armState;

    private Boss_FSM _bossFsm;
    private BossUiManager _uiManager;

    private new void Awake()
    {
        _bossFsm = GetComponent<Boss_FSM>();
        _uiManager = FindObjectOfType<BossUiManager>();
        base.Awake();
    }
    
    private void Start()
    {
        currArmour = maxArmour;
    }

    public void Damage(int damageAmount)
    {
        if (armState)
        {
            if (!_uiManager.IsActive(1))
            {
                _uiManager.EnableSlider(1);
            }

            if (currArmour > 0)
            {
                currArmour -= damageAmount;
                float currArmPercentage = (float)currArmour / (float)maxArmour;
                _uiManager.UpdateSlider(1, currArmPercentage);
            }


            if (currArmour <= 0)
            {
                currArmour = 0;
                armState = false;
                _uiManager.DisableSlider(1);
                currArmour = maxArmour;
                _bossFsm.SetState(_bossFsm.stuntState);
            }
        }
        else
        {
            
            TakeDamage(damageAmount);
            _uiManager.UpdateSlider(0, CurrHpPercentage);
        }
    }

    protected override void Die()
    {
        _bossFsm.SetState(_bossFsm.dieState);
        _uiManager.DisableSlider(0);
    }

    public float LostHP()
    {
        return 0;
    }
}
