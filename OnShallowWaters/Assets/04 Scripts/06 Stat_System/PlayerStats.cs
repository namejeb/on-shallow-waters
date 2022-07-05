using System.Collections;
using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;


public class PlayerStats : CharacterStats, IShopCustomer, IDamageable
{
    [SerializeField] private Stat movementSpeed;
    [SerializeField] private Stat defense;
    
    private float _atkPercent = 1f;
    private float _mvmntSpdMutliplier = 1f;
    private float _defMutliplier = 1f;
    
    private float _damageReduction = 1f;
    
    private float _critChance = .3f;
    private float _critDamage = 1.5f;
    
    private float _atkSpeed = 1f;
    
    public Stat MovementSpeed { get => movementSpeed; }
    public Stat Defense { get => defense; }
    
    public float AtkPercent { get => _atkPercent; }
    public float MovementSpeedMultiplier  { get => _mvmntSpdMutliplier; }
    public float DefMultiplier { get => _defMutliplier; }
    
    public float AtkSpeed { get => _atkSpeed; }
    public float CritChance { get => _critChance; }
    public float CritDamage { get => _critDamage; }

    public float DamageReduction { get => _damageReduction; }

    private BoonDamageModifiers _boonDamageModifiers;

    private new void Awake()
    {
        _boonDamageModifiers = GetComponent<BoonDamageModifiers>();
    }
    
    protected override void Die()
    {
        //game end logics
    }

    public void IncreaseAtkPercent(float multiplierToSet)
    {
        _atkPercent = multiplierToSet;
    }
    
    public void IncreaseDamageReduction(float multiplierToSet)
    {
        _damageReduction = multiplierToSet;
    }

    public void IncreaseCritChance(float multiplierToSet)
    {
        _critChance = multiplierToSet;
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
    
    public void IncreaseCritDmg(float multiplierToSet)
    {
        _critDamage *= multiplierToSet;
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

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            RemoveModifier(Atk, 25.9f);
        }

        if (Input.GetKeyDown("a"))
        {
            AddModifier(Atk , 25.9f);
        }
    }
}