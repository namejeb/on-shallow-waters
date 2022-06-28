using System.Collections;
using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;


public class PlayerStats : CharacterStats, IShopCustomer, IDamageable
{
    [SerializeField] private Stat atkPercent;
    private float _atkSpeed = 1f;
    
    private float _critChance = .3f;
    private float _critDamage = 1.5f;
    
    [SerializeField] private Stat movementSpeed;
    private float _mvmntSpdMutliplier = 1f;
    
    [SerializeField] private Stat defense;
    private float _defMutliplier = 1f;
    
    private float _damageReduction = 1f;
    
    
    public Stat AtkPercent { get => atkPercent; }
    public float AtkSpeed { get => _atkSpeed; }
    public float CritChance { get => _critChance; }
    public float CritDamage { get => _critDamage; }
    public Stat MovementSpeed { get => movementSpeed; }
    public float MovementSpeedMultiplier  { get => _mvmntSpdMutliplier; }
    public Stat Defense { get => defense; }
    public float DefMultiplier { get => _defMutliplier; }
    public float DamageReduction { get => _damageReduction; }

    private BoonDamageModifiers _boonDamageModifiers;

    private new void Awake()
    {
        _boonDamageModifiers = GetComponent<BoonDamageModifiers>();
        movementSpeed.ModifyBaseValue(10);
    }
    
    protected override void Die()
    {
        //game end logics
    }
    
    public void IncreaseDamageReduction(float multiplierToSet)
    {
        _damageReduction = multiplierToSet;
    }

    public void IncreaseCritChance(float multiplierToSet)
    {
        _critChance = multiplierToSet;
    }

    public void IncreaseCritDmg(float multiplier)
    {
        _critDamage *= multiplier;
    }

    public void IncreaseDef(float multiplierToSet)
    {
        _defMutliplier = multiplierToSet;
    }
    public void IncreaseMvmntSpd(float multiplierToSet)
    {
        _mvmntSpdMutliplier = multiplierToSet;
    }

    public void IncreaseAtkSpd(float multiplierToSet)
    {
        _atkSpeed = multiplierToSet;
    }

    public void IncreaseMaxHp(float multiplier)
    {
        MaxHp *= multiplier;
    }
    
    public void BoughtItem(ShopItem.ItemType itemType, CurrencyType currencyType)
    {
        if (currencyType == CurrencyType.GOLD)
        {
            HandleGoldShopUpgrades(itemType);
        } 
        else
        {
            HandleSoulShopUpgrades(itemType);
        }
    }
    public bool TrySpendCurrency(CurrencyType currencyType, int amountToSpend)
    {
        if (CurrencySystem.currencyDict[currencyType] > amountToSpend)
        {
            CurrencySystem.RemoveCurrency(currencyType, amountToSpend);
            
            return true;
        }

        return false;
    }

    private void HandleGoldShopUpgrades(ShopItem.ItemType itemType)
    {
        switch (itemType)
        {
            //case ShopItem.ItemType.ATK: UpgradeAtk(); break;
        }
    }

    private void HandleSoulShopUpgrades(ShopItem.ItemType itemType)
    {
        //print("Soul Shop not implemented yet");
        switch (itemType)
        {
            //souls shop upgrades   
        }
    }

    public IEnumerator RegenLoop(int regenHp, int regenArm, int regenCount, float regenPerSeconds)
    {
        for (int i = 0; i < regenCount; i++)
        {
            yield return new WaitForSeconds(regenPerSeconds);
            currHp += regenHp;
            // player hp bar should updated
            Debug.Log(currHp);
        }

    }
    
    public void Damage(int damageAmount)
    {
        int dmg = (int) ReceiveIncomingDmg(damageAmount);
        TakeDamage(dmg);
    }
    
    private float ReceiveIncomingDmg(float incomingDamage)
    {
        incomingDamage *= (100 / (100 + (DefMultiplier * (Defense.CurrentValue + 0))) * DamageReduction);

        if (_boonDamageModifiers.dmgReductionActivated)
        {
            if (CurrHpPercentage < _boonDamageModifiers.dmgReductionActivationThreshold)
            {
                //decrease by 25%
                incomingDamage *= (1 - .25f);
            }
        }
        
        return incomingDamage;
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
}