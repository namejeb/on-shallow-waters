using System;
using UnityEngine;


public class BoonEffects : MonoBehaviour {
    private PlayerStats _playerStats;

    [System.Serializable]
    private class StatIncreaseAmounts
    {
        [HideInInspector] public Stat stat;
        public float[] increaseAmounts;
        [HideInInspector] public int tracker;

        public StatIncreaseAmounts(int size)
        {
            this.increaseAmounts = new float[size];
        }
    }
    
    [System.Serializable]
    private class StatIncreaseAmountsInt
    {
        [HideInInspector] public int statInt;
        public float[] increaseAmounts;
        [HideInInspector] public int tracker;

        public StatIncreaseAmountsInt(int size)
        {
            this.increaseAmounts = new float[size];
        }
    }
    [Header("Increase Amounts (Combat): ")]
    [SerializeField] private StatIncreaseAmounts atkPercent = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts atkSpd = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts critChance = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts critDmg = new StatIncreaseAmounts(3);


    [Space] [Space] 
    [Header("Increase Amounts (Survival): ")] 
    [SerializeField] private float[] maxHpIncreaseAmounts = new float[3];
    private int _maxHpTracker;
    
 //   [SerializeField] private StatIncreaseAmountsInt maxHp = new StatIncreaseAmountsInt(3);
    [SerializeField] private StatIncreaseAmounts defense = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts mvmntSpd = new StatIncreaseAmounts(4);
    [SerializeField] private StatIncreaseAmounts dmgReduction = new StatIncreaseAmounts(2);
    [SerializeField] private StatIncreaseAmounts dmgReductionWhenLowHp = new StatIncreaseAmounts(2);
    
    
    private void OnValidate()
    {
        for (int i = 0; i < 3; i++)
        {
            atkPercent.increaseAmounts[i] = Mathf.Clamp(atkPercent.increaseAmounts[i], 1, float.MaxValue);
            atkSpd.increaseAmounts[i] = Mathf.Clamp(atkSpd.increaseAmounts[i], 1, float.MaxValue);
            critChance.increaseAmounts[i] = Mathf.Clamp(critChance.increaseAmounts[i], 1, float.MaxValue);
            critDmg.increaseAmounts[i] = Mathf.Clamp(critDmg.increaseAmounts[i], 1, float.MaxValue);
            maxHpIncreaseAmounts[i] = Mathf.Clamp(maxHpIncreaseAmounts[i], 1, float.MaxValue);
            defense.increaseAmounts[i] = Mathf.Clamp(defense.increaseAmounts[i], 1, float.MaxValue);
        }

        for (int i = 0; i < 4; i++)
        {
            dmgReduction.increaseAmounts[i] = Mathf.Clamp(dmgReduction.increaseAmounts[i], 1, float.MaxValue);
        }

        for (int i = 0; i < 2; i++)
        {
            dmgReductionWhenLowHp.increaseAmounts[i] = Mathf.Clamp(dmgReductionWhenLowHp.increaseAmounts[i], 1, float.MaxValue);
        }
    }

    private void InitIncreaseAmounts()
    {
        atkPercent.stat = _playerStats.AtkPercent;
        atkSpd.stat = _playerStats.AtkSpeed;
        
        critChance.stat = _playerStats.CritChance;
        critDmg.stat = _playerStats.CritDamage;
        
        defense.stat = _playerStats.Defense;
        
        dmgReduction.stat = _playerStats.DamageReduction;
        dmgReductionWhenLowHp.stat = _playerStats.DamageReduction;
    }
    private void Start()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
        InitIncreaseAmounts();
    }
    
    //------Combat------
    // ATK % increase 50%/85%/120%/130%/140%/150%
    public void UpgradeAtkPercent()                        //---5
    {
        UpgradeStat(atkPercent);
        print("atk:" + _playerStats.AtkPercent.CurrentValue);
    }

    //ATK speed increase 15%/25%/30%/35%/40%/45%
    public void UpgradeAtkSpd()                     //---5
    {
        UpgradeStat(atkSpd);
    }
    
        
    //Crit chance increase 15%/25%/30%/35%/40%/45% ( Normal crit deal 50% more dmg)
    public void UpgradeCritChance()                 //---5
    {
        UpgradeStat(critChance);
    }

    //Crit dmg increase 20%/30%/40%/50%/60%/70%
    public void UpgradeCritDamage()                 //---5
    {
        UpgradeStat(critDmg);
    }

             
    //Deal dmg when enemy's armor break
    //sus - Deal 100 true dmg when enemy's armor break    
    public void DmgWhenArmorBreak()                 //---4
    {
        
    }

    //Deal 20% more dmg to enemy's armor
    public void DamageToArmorIncrease()             //---3 
    {
        
    }
    
    //Deal 40% more dmg when there is only one enemy
    public void SingleEnemyDmgIncrease()            //---3 
    {
        
    }
    
    //Undamaged enemies will receive 100% more damage
    public void FirstTimeDmgBonus()                 //---3 
    {
        
    }
    
    //------Survival------
    
    //Increase max hp by 30%/70%/110% 
    public void IncreaseMaxHp()                     //---5 
    {
        _playerStats.IncreaseMaxHp(maxHpIncreaseAmounts[_maxHpTracker] ); print($"Current Max: {_playerStats.MaxHp}");
        _maxHpTracker++;
    }
    
    //Increase defense by 30%/50%/70%
    public void IncreaseDefense()                     //---5 
    {
        
    }

    //Increase movement speed by 15%/25%/35%/50%   
    public void IncreaseMovementSpeed()             //---5 
    {
       // UpgradeStat(_playerStats.MovementSpeed, mvmntSpdIncreaseAmounts, ref _mvmntSpdTracker);
       UpgradeStat(mvmntSpd);
    }
    //Reduce damage taken by 10%/20%
    public void ReduceDamageTaken()                 //---2
    {
        
    }
    
    //Reduce 50% dmg taken while 30% hp or lower
    public void ReduceDamageWhenHpLow()         //---2
    {
        
    }

    //Utility
    private void UpgradeStat(StatIncreaseAmounts statIncreaseAmounts)
    {
        Stat stat = statIncreaseAmounts.stat;
        
        Vector2Int modifierValues = CalcModifierValues(statIncreaseAmounts);
        stat.AddModifier( modifierValues.x );
        stat.RemoveModifier( modifierValues.y );
        statIncreaseAmounts.tracker++;
    }

    //Get modifier to remove and add for a Stat
    private Vector2Int CalcModifierValues(StatIncreaseAmounts statIncreaseAmounts)
    {
        //data
        int tracker = statIncreaseAmounts.tracker;
        Stat stat = statIncreaseAmounts.stat;
        float[] increaseAmounts = statIncreaseAmounts.increaseAmounts;
        
        //calculation
        float revertedValue = statIncreaseAmounts.stat.CurrentValue;
        int modifierToRemove = 0;
        
        if (tracker > 0)
        {
            float lastMultiplier = increaseAmounts[tracker - 1];
            revertedValue = stat.CurrentValue / lastMultiplier;

            float valWithLastMultiplier = revertedValue * lastMultiplier;
            modifierToRemove = Mathf.RoundToInt(GetDifference(revertedValue, valWithLastMultiplier));
        }
        float newValue = revertedValue * increaseAmounts[tracker];
        int modifierToAdd =  Mathf.RoundToInt(GetDifference(newValue, revertedValue));

        Vector2Int modifierValues = new Vector2Int(modifierToAdd, modifierToRemove);
        return modifierValues;
    }
    
    private float GetDifference(float val1, float val2)
    {
        return Mathf.Abs(val1 - val2);
    }

    private int GetIncreaseAmount(StatIncreaseAmounts statIncreaseAmounts)
    {
        return (int) ( statIncreaseAmounts.increaseAmounts[statIncreaseAmounts.tracker] * 100 );
    }
    
    
    
    // //Increase 5% damage every time you attack
    // public void SequentialDmgIncrease()             //---2
    // {
    //     
    // }

    // //Increase dodge chance 10%/20%/25%
    // public void IncreaseDodgeChance()               //---\
    // {
    //     
    // }

    // //Deal 25% more dmg when you are 40% hp or lower
    // public void NearDeathDmgIncrease()              //---2
    // {
    //     
    // }
    //

    // //Dash +1/+2
    // public void IncreaseDashTimes()             //---\
    // {
    //     
    // }

    // //Resist 20%/50%
    // public void ReduceDOTDmg()                  //---\
    // {
    //     
    // }

    // //Killing an enemy heals 10hp
    // public void LifeSteal()                     //---4 (* art asset)
    // {
    //     
    // }
    
    
    // //------Bonus------
    // //Increase souls received by 30%/50%/70%
    // public void GetMoreSouls()              //---4
    // {
    //     
    // }
    //
    // //Increase gold received by 30%/50%/70
    // public void GetMoreGold()               //---4
    // {
    //     
    // }
    //
    // //Shop 15%/25% discount
    // public void ShopDiscount()                           //---1 (* art asset)
    // {
    //     
    // }
    //
    // //30% chance to encounter a treasure chest
    // public void IncreaseTreasureChestChance()            //---\
    // {
    //     
    // }
    //
    // //20% chance to encounter a challenge statue
    // public void IncreaseChallengeStatueChance()         //---\
    // {
    //     
    // }
    //
    // //Deal bonus damage based on amount of souls you got
    // public void BonusDmgBasedOnSouls()                  //---3
    // {
    //     
    // }
    //
    // //20% increase spawning coin vase
    // public void IncreaseCoinVaseChance()            //---\
    // {
    //     
    // }

}
