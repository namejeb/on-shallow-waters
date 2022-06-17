using UnityEngine;


public class BoonEffects : MonoBehaviour
{
    private PlayerStats _playerStats;
    
    [Header("Increase Amounts: ")]
    [SerializeField] private float[] atkIncreaseAmounts = new float[3];
    private int _atkTracker;

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
        }
    }

    private void Start()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
    }
    
    //------Combat------
    public void UpgradeAtk()
    {
        UpgradeStat(_playerStats.Atk, atkIncreaseAmounts, _atkTracker);
        _atkTracker++;
    }

    public void UpgradeAtkSpd()
    {
        UpgradeStat(_playerStats.AtkSpeed, atkSpeedIncreaseAmounts, _atkSpeedTracker);
        _atkSpeedTracker++;
    }
    
    public void UpgradeCritDamage()
    {
        UpgradeStat(_playerStats.CritDamage, critDamageIncreaseAmounts, _critDamageTracker);
        _critDamageTracker++;
    }
    
    public void UpgradeCritChance()
    {
        UpgradeStat(_playerStats.CritChance, critChanceIncreaseAmounts, _critChanceTracker);
        _critChanceTracker++;
    }

    //Last heavy attack becomes AOE (or bigger range)
    public void LastHeavyAtkAOE()
    {
        
    }

    //Increase 5% damage every time you attack
    public void SequentialDmgIncrease()
    {
        
    }

    //Deal dmg when enemy's armor break
    public void DmgWhenArmorBreak()
    {
        
    }

    //Deal 50% more dmg to enemy's armor
    public void DamageToArmorIncrease()
    {
        
    }
    
    //Deal 30% more dmg when there is only one enemy
    public void SingleEnemyDmgIncrease()
    {
        
    }
    
    //Deal 25% more dmg when you are 40% hp or lower
    public void NearDeathDmgIncrease()
    {
        
    }
    
    //Undamaged enemies will receive 100% more damage
    public void FirstTimeDmgBonus()
    {
        
    }
    
    //------Survival------
    
    //Increase max hp by 25
    public void IncreaseMaxHp()
    {
        
    }

    //Increase speed by 20%/50%/70%
    public void IncreaseMovementSpeed()
    {
        
    }

    //Increase dodge chance 10%/20%/25%
    public void IncreaseDodgeChance()           //---\
    {
        
    }

    //Reduce damage taken by 10%/20%/30%
    public void ReduceDamageTaken()
    {
        
    }

    //Dash +1/+2
    public void IncreaseDashTimes()             //---\
    {
        
    }

    //Resist 20%/50%
    public void ReduceDOTDmg()
    {
        
    }

    //Reduce 50% dmg taken while 30% hp or lower
    public void ReduceDamageWhenHpLow()
    {
        
    }

    //Killing an enemy heals 10hp
    public void LifeSteal()
    {
        
    }
    
    
    //------Bonus------
    //Increase souls received by 30%/50%/70%
    public void GetMoreSouls()
    {
        
    }

    //Increase gold received by 30%/50%/70
    public void GetMoreGold()
    {
        
    }

    //Shop 15%/25% discount
    public void ShopDiscount()                           //---\
    {
        
    }

    //30% chance to encounter a treasure chest
    public void IncreaseTreasureChestChance()            //---x
    {
        
    }

    //20% chance to encounter a challenge statue
    public void IncreaseChallengeStatueChance()         //---x
    {
        
    }
    
    //Deal bonus damage based on amount of souls you got
    public void BonusDmgBasedOnSouls()
    {
        
    }
    
    //20% increase spawning coin vase
    public void IncreaseCoinVaseChance()
    {
        
    }

    
    
    //Utility
    private void UpgradeStat(Stat stat, float[] increaseAmounts, int tracker)
    {
        Vector2Int modifierValues = CalcModifierValues(stat, increaseAmounts, tracker);
        stat.AddModifier( modifierValues.x );
        stat.RemoveModifier( modifierValues.y );
        
       // print($"(Added: {modifierValues.x}, Removed: {modifierValues.y})");
    }
    
    
    //Get modifier to remove and add for a Stat
    private Vector2Int CalcModifierValues(Stat stat, float[] increaseAmounts, int tracker)
    {
        float revertedValue = stat.CurrentValue;
        
        int modifierToRemove = 0;

        if (tracker > 0)
        {
            float lastMultipliedValue = increaseAmounts[tracker - 1] ;
            revertedValue = stat.CurrentValue / lastMultipliedValue;

            modifierToRemove = Mathf.RoundToInt(stat.PrevModifierByBoon);
        }
        float newValue = revertedValue * increaseAmounts[tracker];
        
        int modifierToAdd =  Mathf.RoundToInt(GetDifference(newValue, revertedValue));
        stat.PrevModifierByBoon = modifierToAdd;

        Vector2Int modifierValues = new Vector2Int(modifierToAdd, modifierToRemove);
        return modifierValues;
    }

    private float GetDifference(float newValue, float revertedValue)
    {
        return newValue - revertedValue;
    }
}
