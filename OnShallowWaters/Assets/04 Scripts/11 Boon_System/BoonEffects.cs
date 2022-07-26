using System.Collections;
using UnityEngine;
using System.Collections.Generic;


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
    public string boonItemName;
    [HideInInspector] public float val;
    public float[] increaseAmounts = new float[3];
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
    [SerializeField] private BoonEffectsManager boonEffectsManager;
    

    [Space] public int boonToUse;


    [SerializeField] private BoonItemsSO boonItemsSo;

    [Header("Gets id from BoonItemSo, so make sure each index corresponds to the effects in BoonItemSo")]
   // [SerializeField] private List<StatIncreaseAmountsFloat> statIncreaseAmounts;


    private Hashtable HIncreaseAmounts = new Hashtable();


    private void OnValidate()
    {
      //  InitStatIncreaseAmounts();
    }

    private void Start()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
      //  InitStatIncreaseAmounts();
      //  InitHash();
    }

    // private void InitHash()
    // {
    //     HIncreaseAmounts.Clear();
    //
    //     for(int i = 0; i < statIncreaseAmounts.Count; i++){
    //         HIncreaseAmounts.Add(statIncreaseAmounts[i].idFromBoonItem, statIncreaseAmounts[i]);
    //     }
    // }
    //
    // private void InitStatIncreaseAmounts()
    // {
    //     //init id
    //     for (int i = 0; i < boonItemsSo.boonItems.Length; i++)
    //     {
    //         statIncreaseAmounts[i].idFromBoonItem = boonItemsSo.boonItems[i].id;
    //     }
    // }
    //
    public void HandleEffectActivation(int boonItemId)
    {
       // boonItemId = boonToUse;
        
        switch (boonItemId)
        {
            case 0: boonEffectsManager.MaxHp();      break;        //--x
            case 1: boonEffectsManager.Defense();  break;          //--x
            case 2: boonEffectsManager.MovementSpeed();  break;       //--x
            case 3: boonEffectsManager.LowHpDmgReduction(); break;    //--x
            
            case 4: boonEffectsManager.ArmorDmgBonus();  break;       //--x
            case 5: boonEffectsManager.DmgWhenArmorBreak(); break;
            case 6: boonEffectsManager.IncreaseDmgIncreaseSingleEnemy(); break;
            case 7: boonEffectsManager.FirstTimeDmgBonus(); break;
            
            case 8: boonEffectsManager.UpgradeAtkPercent(); break;   //---x
            case 9: boonEffectsManager.UpgradeAtkSpeed();      break;        //--x
            case 10: boonEffectsManager.UpgradeCritChance();  break;        //--x
            case 11: boonEffectsManager.UpgradeCritDamage();  break;        //--x
        }
    }



    //------Survival------
    //Increase max hp by 30%/70%/110% 
   //  private void IncreaseMaxHp()
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(0);
   //      _playerStats.IncreaseMaxHp( s.GetIncreaseAmount() );
   //  }
   //  
   //  //Increase defense by 30%/50%/70%
   //  private void IncreaseDefense()                   
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(1);
   //      _playerStats.IncreaseDef( s.GetIncreaseAmount() );
   //  }
   //
   //  //Increase movement speed by 15%/25%/35%/50%   
   //  private void IncreaseMovementSpeed()              
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(2);
   //      _playerStats.IncreaseMvmntSpd( s.GetIncreaseAmount() );
   //  }
   //
   //  //Reduce 25% dmg taken while 30%/40% hp or lower.
   //  private void ReduceDamageWhenHpLow()            
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(3);
   //     _playerStats.IncreaseDamageReduction( s.GetIncreaseAmount() );
   //    // PlayerHandler.Instance.BoonDamageModifiers.EnableDmgReductionWhenLowHp( s.GetIncreaseAmount() );
   //  }
   //  
   //  
   //
   //
   //  //------Combat------
   //  // ATK % increase 50%/85%/120%
   //  private void UpgradeAtkPercent()
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(8);
   //      _playerStats.IncreaseAtkPercent( s.GetIncreaseAmount() ); // print(_playerStats.AtkPercent);
   //  }
   //
   //  //ATK speed increase 15%/25%/30%
   //  private void UpgradeAtkSpd()                 
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(9);
   //      _playerStats.IncreaseAtkSpd( s.GetIncreaseAmount() ); // print(_playerStats.AtkSpeed);
   //  }
   //  
   //      
   //  //Crit chance increase 15%/25%/30% ( Normal crit deal 50% more dmg)
   //  private void UpgradeCritChance()          
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(10);
   //     _playerStats.IncreaseCritChance(s.GetIncreaseAmount());
   //  }
   //
   //  //Crit dmg increase 20%/30%/40%
   //  private void UpgradeCritDamage()              
   //  { 
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(11);
   //      _playerStats.IncreaseCritDmg(s.GetIncreaseAmount());
   //  }
   //
   //
   //  //Deal 20% more dmg to enemy's armor
   //  private void DmgToArmorIncrease()          
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(4);
   //    //  PlayerHandler.Instance.BoonDamageModifiers.EnableExtraShieldDmg( s.GetIncreaseAmount() );
   //  }
   //
   //
   //  //Deal dmg when enemy's armor break
   //  //sus - Deal 100 true dmg when enemy's armor break    
   //  private void DmgWhenArmorBreak()             
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(5);
   //  //    PlayerHandler.Instance.BoonDamageModifiers.EnableDmgWhenShieldBreak(s.GetIncreaseAmount());
   //  }
   //
   //  
   //  //Deal 40% more dmg when there is only one enemy
   //  private void SingleEnemyDmgIncrease()        
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(6);
   // //     PlayerHandler.Instance.BoonDamageModifiers.EnableSingleEnemyDmgIncrease( s.GetIncreaseAmount() );
   //  }
   //  
   //  //Undamaged enemies will receive 100% more damage
   //  private void FirstTimeDmgBonus()             
   //  {
   //      StatIncreaseAmountsFloat s = GetStatIncreaseAmountFloat(7);
   //    //  PlayerHandler.Instance.BoonDamageModifiers.EnableFirstTimeDmgBonus( s.GetIncreaseAmount() );
   //  }
   //  
   //
   //
   //  //Utility
   //  private void UpgradeStat(StatIncreaseAmounts statIncreaseAmounts)
   //  {
   //      Stat stat = statIncreaseAmounts.stat;
   //      
   //      Vector2Int modifierValues = CalcModifierValues(statIncreaseAmounts);
   //      stat.AddModifier( modifierValues.x );
   //      stat.RemoveModifier( modifierValues.y );
   //      
   //      statIncreaseAmounts.tracker++;
   //  }
   //  
   //  private StatIncreaseAmountsFloat GetStatIncreaseAmountFloat(int boonItemId)
   //  {
   //      StatIncreaseAmountsFloat s = null;
   //
   //      for (int i = 0; i < statIncreaseAmounts.Count; i++)
   //      {
   //          if (statIncreaseAmounts[i].idFromBoonItem == boonItemId)
   //          {
   //              s = statIncreaseAmounts[i];
   //          }
   //      }
   //
   //      return s;
   //  }
   //
   //  private void UpdateTracker(StatIncreaseAmountsFloat s)
   //  {
   //      s.tracker++;
   //  }
   //
   //  
   //  //Get modifier to remove and add for a Stat
   //  private Vector2Int CalcModifierValues(StatIncreaseAmounts statIncreaseAmounts)
   //  {
   //      //data
   //      int tracker = statIncreaseAmounts.tracker;
   //      Stat stat = statIncreaseAmounts.stat;
   //      float[] increaseAmounts = statIncreaseAmounts.increaseAmounts;
   //      
   //      //calculation
   //      float revertedValue = statIncreaseAmounts.stat.CurrentValue;
   //      int modifierToRemove = 0;
   //      
   //      if (tracker > 0)
   //      {
   //          float lastMultiplier = increaseAmounts[tracker - 1];
   //          revertedValue = stat.CurrentValue / lastMultiplier;
   //
   //          float valWithLastMultiplier = revertedValue * lastMultiplier;
   //          modifierToRemove = Mathf.RoundToInt(GetDifference(revertedValue, valWithLastMultiplier));
   //      }
   //      float newValue = revertedValue * increaseAmounts[tracker];
   //      int modifierToAdd =  Mathf.RoundToInt(GetDifference(newValue, revertedValue));
   //
   //      Vector2Int modifierValues = new Vector2Int(modifierToAdd, modifierToRemove);
   //      return modifierValues;
   //  }
   //  
   //  private float GetDifference(float val1, float val2)
   //  {
   //      return Mathf.Abs(val1 - val2);
   //  }
   //
   //
   //  public float GetStatIncreaseAmounts(int id)
   //  {
   //      if (HIncreaseAmounts[id] is StatIncreaseAmounts)
   //      {
   //          StatIncreaseAmounts sia = (StatIncreaseAmounts) HIncreaseAmounts[id];
   //          return sia.GetIncreaseAmount();
   //      }
   //      if (HIncreaseAmounts[id] is StatIncreaseAmountsFloat)
   //      {
   //          StatIncreaseAmountsFloat sia = (StatIncreaseAmountsFloat) HIncreaseAmounts[id];
   //          return sia.GetIncreaseAmount();
   //      }
   //      if (HIncreaseAmounts[id] is StatIncreaseAmountsInt)
   //      {
   //          StatIncreaseAmountsInt sia = (StatIncreaseAmountsInt) HIncreaseAmounts[id];
   //          return sia.GetIncreaseAmount();
   //      }
   //      return 0f;
   //  }
    
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
