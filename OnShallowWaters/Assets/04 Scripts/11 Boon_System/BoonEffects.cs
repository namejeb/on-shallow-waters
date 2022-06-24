using Unity.Collections;
using UnityEngine;


public class BoonEffects : MonoBehaviour {
    private PlayerStats _playerStats;
    
    [Header("Increase Amounts: ")]
    [SerializeField] private float[] atkIncreaseAmounts = new float[3];
    [SerializeField] private int _atkTracker;

    [SerializeField] private float[] atkSpeedIncreaseAmounts = new float[3];
    private int _atkSpeedTracker;
    
    [SerializeField] private float[] critChanceIncreaseAmounts = new float[3];
    private int _critChanceTracker;
    
    [SerializeField] private float[] critDamageIncreaseAmounts = new float[3];
    private int _critDamageTracker;
    
    private void OnValidate()
    {
        for (int i = 0; i < 3; i++)
        {
            atkIncreaseAmounts[i] = Mathf.Clamp(atkIncreaseAmounts[i], 1, float.MaxValue);
            atkSpeedIncreaseAmounts[i] = Mathf.Clamp(atkSpeedIncreaseAmounts[i], 1, float.MaxValue);
            critChanceIncreaseAmounts[i] = Mathf.Clamp(critChanceIncreaseAmounts[i], 1, float.MaxValue);
            critDamageIncreaseAmounts[i] = Mathf.Clamp(critDamageIncreaseAmounts[i], 1, float.MaxValue);
        }
    }
    private void Start()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
    }
    
    //------Combat------
    public void UpgradeAtk()                        //---5
    {
        UpgradeStat(_playerStats.Atk, atkIncreaseAmounts, ref _atkTracker);
        print("atk:" + _playerStats.Atk.CurrentValue);
    }

    public void UpgradeAtkSpd()                     //---5
    {
        UpgradeStat(_playerStats.AtkSpeed, atkSpeedIncreaseAmounts, ref _atkSpeedTracker);
    }
    
    public void UpgradeCritDamage()                 //---5
    {
        UpgradeStat(_playerStats.CritDamage, critDamageIncreaseAmounts, ref _critDamageTracker);
    }
    
    public void UpgradeCritChance()                 //---5
    {
        UpgradeStat(_playerStats.CritChance, critChanceIncreaseAmounts,  ref _critChanceTracker);
    }
    
    //Increase 5% damage every time you attack
    public void SequentialDmgIncrease()             //---2
    {
        
    }

    //Deal dmg when enemy's armor break
    public void DmgWhenArmorBreak()                 //---4
    {
        
    }

    //Deal 50% more dmg to enemy's armor
    public void DamageToArmorIncrease()             //---3 
    {
        
    }
    
    //Deal 30% more dmg when there is only one enemy
    public void SingleEnemyDmgIncrease()            //---3 
    {
        
    }
    
    //Deal 25% more dmg when you are 40% hp or lower
    public void NearDeathDmgIncrease()              //---2
    {
        
    }
    
    //Undamaged enemies will receive 100% more damage
    public void FirstTimeDmgBonus()                 //---3 
    {
        
    }
    
    //------Survival------
    
    //Increase max hp by 25
    public void IncreaseMaxHp()                     //---5 
    {
        
    }

    //Increase speed by 20%/50%/70%
    public void IncreaseMovementSpeed()             //---5 
    {
        
    }

    //Increase dodge chance 10%/20%/25%
    public void IncreaseDodgeChance()               //---\
    {
        
    }

    //Reduce damage taken by 10%/20%/30%
    public void ReduceDamageTaken()                 //---2
    {
        
    }

    //Dash +1/+2
    public void IncreaseDashTimes()             //---\
    {
        
    }

    //Resist 20%/50%
    public void ReduceDOTDmg()                  //---\
    {
        
    }

    //Reduce 50% dmg taken while 30% hp or lower
    public void ReduceDamageWhenHpLow()         //---2
    {
        
    }

    //Killing an enemy heals 10hp
    public void LifeSteal()                     //---4 (* art asset)
    {
        
    }
    
    
    //------Bonus------
    //Increase souls received by 30%/50%/70%
    public void GetMoreSouls()              //---4
    {
        
    }

    //Increase gold received by 30%/50%/70
    public void GetMoreGold()               //---4
    {
        
    }

    //Shop 15%/25% discount
    public void ShopDiscount()                           //---1 (* art asset)
    {
        
    }

    //30% chance to encounter a treasure chest
    public void IncreaseTreasureChestChance()            //---\
    {
        
    }

    //20% chance to encounter a challenge statue
    public void IncreaseChallengeStatueChance()         //---\
    {
        
    }
    
    //Deal bonus damage based on amount of souls you got
    public void BonusDmgBasedOnSouls()                  //---3
    {
        
    }
    
    //20% increase spawning coin vase
    public void IncreaseCoinVaseChance()            //---\
    {
        
    }
    
    //Utility
    private void UpgradeStat(Stat stat, float[] increaseAmounts, ref int tracker)
    {
        // Vector2Int modifierValues = CalcModifierValues(stat, increaseAmounts, tracker);
        // stat.AddModifier( modifierValues.x );
        //
        // if (tracker != increaseAmounts.Length - 1)
        // {
        //     stat.RemoveModifier( modifierValues.y );
        //     tracker++;
        // }
        
        Vector2Int modifierValues = CalcModifierValues(stat, increaseAmounts, tracker);
        stat.RemoveModifier( modifierValues.y );
        print(modifierValues.x);
        if (tracker != increaseAmounts.Length - 1)
        {
            stat.AddModifier( modifierValues.x );
            tracker++;
        }
        else
        {
         
            stat.AddModifier(  modifierValues.x * (int)increaseAmounts[tracker]);
        }
        
        // print($"(Added: {modifierValues.x}, Removed: {modifierValues.y})");
    }
    
    
    //Get modifier to remove and add for a Stat
    private Vector2Int CalcModifierValues(Stat stat, float[] increaseAmounts, int tracker)
    {
        float revertedValue = stat.CurrentValue;
        
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
    
}
