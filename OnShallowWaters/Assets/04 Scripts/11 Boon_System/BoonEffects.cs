using System.Collections;
using UnityEngine;

[System.Serializable]
public class StatIncreaseAmounts
{
    public int idFromBoonItem;
    [HideInInspector] public Stat stat;
    public float[] increaseAmounts;
    [HideInInspector] public int tracker = 0;

    public StatIncreaseAmounts(int size)
    {
        this.increaseAmounts = new float[size];
    }
    
    public float GetIncreaseAmount()
    {
        return ( this.increaseAmounts[this.tracker]);
    }
}

[System.Serializable]
public class StatIncreaseAmountsFloat
{
    public int idFromBoonItem;
    [HideInInspector] public float stat;
    public float[] increaseAmounts;
    [HideInInspector] public int tracker = 0;

    public StatIncreaseAmountsFloat (int size)
    {
        this.increaseAmounts = new float[size];
    }
    
    public float GetIncreaseAmount()
    {
        return ( this.increaseAmounts[this.tracker]);
    }
}

[System.Serializable]
public class StatIncreaseAmountsInt
{
    public int idFromBoonItem;
    [HideInInspector] public int stat;
    public int[] increaseAmounts;
    [HideInInspector] public int tracker = 0;

    public StatIncreaseAmountsInt (int size)
    {
        this.increaseAmounts = new int[size];
    }
    
    public float GetIncreaseAmount()
    {
        return ( this.increaseAmounts[this.tracker]);
    }
}
public class BoonEffects : MonoBehaviour {
    
    private PlayerStats _playerStats;

    [Space] public int boonToUse;
    
    [Space] 
    [Header("Effects w/ arrays: ")]
    [Header("Increase Amounts (Combat): ")]
    [SerializeField] private StatIncreaseAmountsFloat shieldExtraDmgBonus = new StatIncreaseAmountsFloat(3);
    [SerializeField] private StatIncreaseAmountsInt shieldBreakTrueDmg = new StatIncreaseAmountsInt(3);
    [SerializeField] private StatIncreaseAmountsFloat dmgIncreaseSingleEnemy = new StatIncreaseAmountsFloat(3);
    [SerializeField] private StatIncreaseAmountsFloat firstTimeDmgBonus = new StatIncreaseAmountsFloat(3);

    [Space]
    [SerializeField] private StatIncreaseAmounts atkPercent = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts atkSpd = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts critChance = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts critDmg = new StatIncreaseAmounts(3);



    [Space] 
    [Header("Increase Amounts (Survival): ")] 
    [SerializeField] private float[] maxHpIncreaseAmounts = new float[3];
    private int _maxHpTracker;
    [SerializeField] private StatIncreaseAmounts defense = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts mvmntSpd = new StatIncreaseAmounts(3);
    [SerializeField] private StatIncreaseAmounts dmgReductionWhenLowHp = new StatIncreaseAmounts(3);

    [Space] [Space] [Space] 
    [Header("Increase Amounts (Special): ")] 

    private Hashtable HIncreaseAmounts = new Hashtable();
    private StatIncreaseAmounts[] _statIncreaseAmounts;

   
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
            
            dmgReductionWhenLowHp.increaseAmounts[i] = Mathf.Clamp(dmgReductionWhenLowHp.increaseAmounts[i], 1, float.MaxValue);
            
            mvmntSpd.increaseAmounts[i] = Mathf.Clamp(mvmntSpd.increaseAmounts[i], 1, float.MaxValue);
        }
    }
    
    private void Start()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
        InitStatIncreaseAmounts();
        InitHash();
    }

    private void InitHash()
    {
        HIncreaseAmounts.Clear();
        
        HIncreaseAmounts.Add(atkPercent.idFromBoonItem, atkPercent);
        HIncreaseAmounts.Add(atkSpd.idFromBoonItem, atkSpd);
        HIncreaseAmounts.Add(critChance.idFromBoonItem, critChance);
        HIncreaseAmounts.Add(critDmg.idFromBoonItem, critDmg);
        HIncreaseAmounts.Add(defense.idFromBoonItem, defense);
        HIncreaseAmounts.Add(mvmntSpd.idFromBoonItem, mvmntSpd);
        HIncreaseAmounts.Add(dmgReductionWhenLowHp.idFromBoonItem, dmgReductionWhenLowHp);
        
        HIncreaseAmounts.Add(shieldExtraDmgBonus.idFromBoonItem, shieldExtraDmgBonus);
        HIncreaseAmounts.Add(shieldBreakTrueDmg.idFromBoonItem, shieldBreakTrueDmg);
        HIncreaseAmounts.Add(dmgIncreaseSingleEnemy.idFromBoonItem, dmgIncreaseSingleEnemy);
        HIncreaseAmounts.Add(firstTimeDmgBonus.idFromBoonItem, firstTimeDmgBonus);
    }
    private void InitStatIncreaseAmounts()
    {
        atkPercent.stat = _playerStats.AtkPercent;
        atkSpd.stat = _playerStats.AtkSpeed;
        
        critChance.stat = _playerStats.CritChance;
        critDmg.stat = _playerStats.CritDamage;
        
        defense.stat = _playerStats.Defense;
        mvmntSpd.stat = _playerStats.MovementSpeed;
        
        //uses the same stats
        dmgReductionWhenLowHp.stat = _playerStats.DamageReduction;

        //based on Base Value

    }
    
    public void HandleEffectActivation(int boonItemId)
    {
        boonItemId = boonToUse;
        switch (boonItemId)
        {
            case 0: IncreaseMaxHp();      break;   //--x
            
            case 1: DmgToArmorIncrease();  break;  //--x
            case 2: DmgWhenArmorBreak();  break;   //--x
            case 3: SingleEnemyDmgIncrease();  break;   //---x
            case 4: FirstTimeDmgBonus();  break;   //--x
            
            case 5: UpgradeAtkPercent();  break;
            case 6: UpgradeAtkSpd();      break;
            case 7: UpgradeCritChance();  break;
            case 8: UpgradeCritDamage();  break;
            case 9: IncreaseDefense();  break;
            case 10: IncreaseMovementSpeed();  break;
            case 11: ReduceDamageWhenHpLow(); break;
        }
    }
    
    //------Combat------
    // ATK % increase 50%/85%/120%
    public void UpgradeAtkPercent()                  //---N
    {
        UpgradeStat(atkPercent);
    }

    //ATK speed increase 15%/25%/30%
    public void UpgradeAtkSpd()                     //---N
    {
        UpgradeStat(atkSpd);
    }
    
        
    //Crit chance increase 15%/25%/30% ( Normal crit deal 50% more dmg)
    public void UpgradeCritChance()                 //---N
    {
        UpgradeStat(critChance);
    }

    //Crit dmg increase 20%/30%/40%
    public void UpgradeCritDamage()                 //---N
    {
        UpgradeStat(critDmg);
    }

             
    //Deal dmg when enemy's armor break
    //sus - Deal 100 true dmg when enemy's armor break    
    public void DmgWhenArmorBreak()                 //---Y
    {
        int effectIndex = shieldBreakTrueDmg.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableDmgWhenShieldBreak(shieldBreakTrueDmg.increaseAmounts[effectIndex]);
        shieldBreakTrueDmg.tracker++;
    }

    //Deal 20% more dmg to enemy's armor
    public void DmgToArmorIncrease()                //---Y 
    {
        int effectIndex = shieldExtraDmgBonus.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableExtraShieldDmg(shieldExtraDmgBonus.increaseAmounts[effectIndex]);
        shieldExtraDmgBonus.tracker++;
    }
    
    //Deal 40% more dmg when there is only one enemy
    public void SingleEnemyDmgIncrease()            //---Y 
    {
        int effectIndex = dmgIncreaseSingleEnemy.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableSingleEnemyDmgIncrease(dmgIncreaseSingleEnemy.increaseAmounts[effectIndex]);
        dmgIncreaseSingleEnemy.tracker++;
    }
    
    //Undamaged enemies will receive 100% more damage
    public void FirstTimeDmgBonus()                 //---Y 
    {
        int effectIndex = firstTimeDmgBonus.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableFirstTimeDmgBonus(firstTimeDmgBonus.increaseAmounts[effectIndex]);
        firstTimeDmgBonus.tracker++;
    }
    
    //------Survival------
    //Increase max hp by 30%/70%/110% 
    public void IncreaseMaxHp()                     //---Y 
    {
        _playerStats.IncreaseMaxHp( maxHpIncreaseAmounts[_maxHpTracker] );
        _maxHpTracker++;
    }
    
    //Increase defense by 30%/50%/70%
    public void IncreaseDefense()                     //---N
    {
        UpgradeStat(defense);
    }

    //Increase movement speed by 15%/25%/35%/50%   
    public void IncreaseMovementSpeed()                 //---Y 
    {
        UpgradeStat(mvmntSpd);
    }
    //Reduce damage taken by 10%/20%
    // public void ReduceDamageTaken()                 //---N
    // {
    //     UpgradeStat(dmgReduction);
    // }
    
    //Reduce 25% dmg taken while 30%/40% hp or lower.
    public void ReduceDamageWhenHpLow()             //---N
    {
        UpgradeStat(dmgReductionWhenLowHp);
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
    
    public float GetMaxHpIncreaseAmount()
    {
        return maxHpIncreaseAmounts[_maxHpTracker];
    }

    public float GetStatIncreaseAmounts(int id)
    {
        if (HIncreaseAmounts[id] is StatIncreaseAmounts)
        {
            StatIncreaseAmounts sia = (StatIncreaseAmounts) HIncreaseAmounts[id];
            return sia.GetIncreaseAmount();
        }
        if (HIncreaseAmounts[id] is StatIncreaseAmountsFloat)
        {
            StatIncreaseAmountsFloat sia = (StatIncreaseAmountsFloat) HIncreaseAmounts[id];
            return sia.GetIncreaseAmount();
        }
        if (HIncreaseAmounts[id] is StatIncreaseAmountsInt)
        {
            StatIncreaseAmountsInt sia = (StatIncreaseAmountsInt) HIncreaseAmounts[id];
            return sia.GetIncreaseAmount();
        }
        return 0f;
    }

    // public float GetFloatIncreaseAmounts(int id)
    // {
    //     return _floatIncreaseAmounts[id - 1];
    // }
    
    
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
