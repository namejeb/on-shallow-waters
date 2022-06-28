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
    [HideInInspector] public float val;
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
    [HideInInspector] public int val;
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
    [SerializeField] private StatIncreaseAmountsFloat atkSpd = new StatIncreaseAmountsFloat(3);
    [SerializeField] private StatIncreaseAmountsFloat critChance = new StatIncreaseAmountsFloat(3);
    [SerializeField] private StatIncreaseAmountsFloat critDmg = new StatIncreaseAmountsFloat(3);



    [Space] 
    [Header("Increase Amounts (Survival): ")] 
    [SerializeField] private float[] maxHpIncreaseAmounts = new float[3];
    private int _maxHpTracker;
    [SerializeField] private StatIncreaseAmountsFloat defMultiplier = new StatIncreaseAmountsFloat(3);
    [SerializeField] private StatIncreaseAmountsFloat mvmntSpdMultiplier = new StatIncreaseAmountsFloat(3);
    [SerializeField] private StatIncreaseAmountsFloat dmgReductionWhenLowHp = new StatIncreaseAmountsFloat(3);

    [Space] [Space] [Space] 
    [Header("Increase Amounts (Special): ")] 

    private Hashtable HIncreaseAmounts = new Hashtable();

    private void OnValidate()
    {
        for (int i = 0; i < 3; i++)
        {
            atkPercent.increaseAmounts[i] = Mathf.Clamp(atkPercent.increaseAmounts[i], 0, float.MaxValue);
            atkSpd.increaseAmounts[i] = Mathf.Clamp(atkSpd.increaseAmounts[i], 0, float.MaxValue);
            
            critChance.increaseAmounts[i] = Mathf.Clamp(critChance.increaseAmounts[i], 0, float.MaxValue);
            critDmg.increaseAmounts[i] = Mathf.Clamp(critDmg.increaseAmounts[i], 0, float.MaxValue);
            
            maxHpIncreaseAmounts[i] = Mathf.Clamp(maxHpIncreaseAmounts[i], 0, float.MaxValue);
            defMultiplier.increaseAmounts[i] = Mathf.Clamp(defMultiplier.increaseAmounts[i], 0, float.MaxValue);
            
            dmgReductionWhenLowHp.increaseAmounts[i] = Mathf.Clamp(dmgReductionWhenLowHp.increaseAmounts[i], 0, float.MaxValue);
            
            mvmntSpdMultiplier.increaseAmounts[i] = Mathf.Clamp(mvmntSpdMultiplier.increaseAmounts[i], 0, float.MaxValue);
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
        HIncreaseAmounts.Add(defMultiplier.idFromBoonItem, defMultiplier);
        HIncreaseAmounts.Add(mvmntSpdMultiplier.idFromBoonItem, mvmntSpdMultiplier);
        HIncreaseAmounts.Add(dmgReductionWhenLowHp.idFromBoonItem, dmgReductionWhenLowHp);
        
        HIncreaseAmounts.Add(shieldExtraDmgBonus.idFromBoonItem, shieldExtraDmgBonus);
        HIncreaseAmounts.Add(shieldBreakTrueDmg.idFromBoonItem, shieldBreakTrueDmg);
        HIncreaseAmounts.Add(dmgIncreaseSingleEnemy.idFromBoonItem, dmgIncreaseSingleEnemy);
        HIncreaseAmounts.Add(firstTimeDmgBonus.idFromBoonItem, firstTimeDmgBonus);
    }
    private void InitStatIncreaseAmounts()
    {
        atkPercent.stat = _playerStats.AtkPercent;
        atkSpd.val = _playerStats.AtkSpeed;
        
        critChance.val = _playerStats.CritChance;
        critDmg.val = _playerStats.CritDamage;
        
        defMultiplier.val = _playerStats.DefMultiplier;
        mvmntSpdMultiplier.val = _playerStats.MovementSpeedMultiplier;
        
        dmgReductionWhenLowHp.val = _playerStats.DamageReduction;
    }
    
    public void HandleEffectActivation(int boonItemId)
    {
        boonItemId = boonToUse;
        switch (boonItemId)
        {
            case 0: IncreaseMaxHp();      break;        //--x
            
            case 1: DmgToArmorIncrease();  break;       //--x
            case 2: DmgWhenArmorBreak();  break;        //--x
            case 3: SingleEnemyDmgIncrease();  break;   //---x
            case 4: FirstTimeDmgBonus();  break;        //--x
            
            case 5: UpgradeAtkPercent();  break;        //---x
            case 6: UpgradeAtkSpd();      break;        //--x
            case 7: UpgradeCritChance();  break;        //--x
            case 8: UpgradeCritDamage();  break;        //--x
            case 9: IncreaseDefense();  break;          //--x
            case 10: IncreaseMovementSpeed();  break;       //--x
            case 11: ReduceDamageWhenHpLow(); break;    //--x
        }
    }
    
    //------Combat------
    // ATK % increase 50%/85%/120%
    public void UpgradeAtkPercent()               
    {
        UpgradeStat(atkPercent);
    }

    //ATK speed increase 15%/25%/30%
    public void UpgradeAtkSpd()                 
    {
        int effectIndex = atkSpd.tracker;
        _playerStats.IncreaseAtkSpd(atkSpd.increaseAmounts[effectIndex]);
        atkSpd.tracker++;
    }
    
        
    //Crit chance increase 15%/25%/30% ( Normal crit deal 50% more dmg)
    public void UpgradeCritChance()          
    {
       // UpgradeStat(critChance);
       int effectIndex = critChance.tracker;
       _playerStats.IncreaseCritChance(critChance.increaseAmounts[effectIndex]);    print(_playerStats.CritChance);
       critChance.tracker++;
    }

    //Crit dmg increase 20%/30%/40%
    public void UpgradeCritDamage()              
    {
      //  UpgradeStat(critDmg);
      int effectIndex = critDmg.tracker;
      _playerStats.IncreaseCritDmg(critDmg.increaseAmounts[effectIndex]);   print(_playerStats.CritDamage);
      critDmg.tracker++;
    }

             
    //Deal dmg when enemy's armor break
    //sus - Deal 100 true dmg when enemy's armor break    
    public void DmgWhenArmorBreak()             
    {
        int effectIndex = shieldBreakTrueDmg.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableDmgWhenShieldBreak(shieldBreakTrueDmg.increaseAmounts[effectIndex]);
        shieldBreakTrueDmg.tracker++;
    }

    //Deal 20% more dmg to enemy's armor
    public void DmgToArmorIncrease()          
    {
        int effectIndex = shieldExtraDmgBonus.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableExtraShieldDmg(shieldExtraDmgBonus.increaseAmounts[effectIndex]);
        shieldExtraDmgBonus.tracker++;
    }
    
    //Deal 40% more dmg when there is only one enemy
    public void SingleEnemyDmgIncrease()        
    {
        int effectIndex = dmgIncreaseSingleEnemy.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableSingleEnemyDmgIncrease(dmgIncreaseSingleEnemy.increaseAmounts[effectIndex]);
        dmgIncreaseSingleEnemy.tracker++;
    }
    
    //Undamaged enemies will receive 100% more damage
    public void FirstTimeDmgBonus()             
    {
        int effectIndex = firstTimeDmgBonus.tracker;
        PlayerHandler.Instance.BoonDamageModifiers.EnableFirstTimeDmgBonus(firstTimeDmgBonus.increaseAmounts[effectIndex]);
        firstTimeDmgBonus.tracker++;
    }
    
    //------Survival------
    //Increase max hp by 30%/70%/110% 
    public void IncreaseMaxHp()                  
    {
        _playerStats.IncreaseMaxHp( maxHpIncreaseAmounts[_maxHpTracker] );
        _maxHpTracker++;
    }
    
    //Increase defense by 30%/50%/70%
    public void IncreaseDefense()                   
    {
        int effectIndex = defMultiplier.tracker;
        _playerStats.IncreaseDef( defMultiplier.increaseAmounts[effectIndex] );
        defMultiplier.tracker++;
    }

    //Increase movement speed by 15%/25%/35%/50%   
    public void IncreaseMovementSpeed()              
    {
        int effectIndex = mvmntSpdMultiplier.tracker;
        _playerStats.IncreaseMvmntSpd( mvmntSpdMultiplier.increaseAmounts[effectIndex]);
        mvmntSpdMultiplier.tracker++;
    }

    //Reduce 25% dmg taken while 30%/40% hp or lower.
    public void ReduceDamageWhenHpLow()            
    {
        int effectindex = dmgReductionWhenLowHp.tracker;
       _playerStats.IncreaseDamageReduction( dmgReductionWhenLowHp.increaseAmounts[effectindex] );
       PlayerHandler.Instance.BoonDamageModifiers.EnableDmgReductionWhenLowHp(dmgReductionWhenLowHp.increaseAmounts[effectindex]);
       dmgReductionWhenLowHp.tracker++;
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
    
    //Reduce damage taken by 10%/20%
    // public void ReduceDamageTaken()                 //---N
    // {
    //     UpgradeStat(dmgReduction);
    // }

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
