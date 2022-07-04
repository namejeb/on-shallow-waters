using System;
using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;


public class Boss_Stats : CharacterStats, IDamageable
{
    public int maxArmour;
    public int currArmour;
    public bool armState;

    //private Boss_FSM _bossFsm;
    StateMachineManager smm;
    private BossUiManager _uiManager;

    public static event Action OnBossDead;

    private new void Awake()
    {
        smm = GetComponent<StateMachineManager>();
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
                smm.SetState(smm.passiveStates[1]);
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
        smm.SetState(smm.passiveStates[0]);
        _uiManager.DisableSlider(0);
        
        if(OnBossDead != null) OnBossDead.Invoke();
    }

    public float LostHP()
    {
        return 0;
    }
}
