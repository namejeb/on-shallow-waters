using System;
using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class Boss_Stats : CharacterStats, IDamageable
{
    public int maxArmour;
    public int currArmour;
    public bool armState;

    public int defense;
    private float buffDefense = 1f;

    EnemyPooler pooler;
    StateMachineManager smm;
    private BossUiManager _uiManager;

    public static event Action OnBossDead;

    private new void Awake()
    {
        smm = GetComponent<StateMachineManager>();
        pooler = FindObjectOfType<EnemyPooler>();
        _uiManager = FindObjectOfType<BossUiManager>();
        base.Awake();
    }
    
    private void Start()
    {
        currArmour = maxArmour;
        
    }
    Transform vfx;
    public void Damage(int damageAmount)
    {
        damageAmount = (int)(damageAmount * 100 / ((25 + (buffDefense * defense))));

        if (armState)
        {
            if (!_uiManager.IsActive(1))
            {
                _uiManager.EnableSlider(1);
                vfx = pooler.GetFromPool(ProjectileType.Boss1Shield);

                vfx.gameObject.SetActive(true);
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
                //shieldVFX.SetActive(false);
                vfx.gameObject.SetActive(false);
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

    public float GetReceivedDamage(float outDamage)
    {
        return outDamage;
    }

    protected override void Die()
    {
        if (smm.CurrentState != smm.passiveStates[0])
        {
            smm.SetState(smm.passiveStates[0]);
            _uiManager.DisableGameObject();
            gameObject.GetComponent<Collider>().enabled = false;

            if (OnBossDead != null) OnBossDead.Invoke();
        }
    }

    public float LostHP()
    {
        return 0;
    }
}
