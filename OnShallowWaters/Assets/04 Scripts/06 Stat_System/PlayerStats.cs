using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : CharacterStats, IShopCustomer
{
    [SerializeField] private Stat atkPercent;
    [SerializeField] private Stat atkSpeed;
    [SerializeField] private Stat critChance;
    [SerializeField] private Stat critDamage;
    [SerializeField] private Stat movementSpeed;
    
    
    [Space][Space]
    [Header("Upgrade Amount:")]
    [SerializeField] private int atkUpgradeAmt = 3;
    [SerializeField] private int defUpgradeAmt = 3;
    
    public Stat AtkPercent { get => atkPercent; }
    public Stat AtkSpeed { get => atkSpeed; }
    public Stat CritChance { get => critChance; }
    public Stat CritDamage { get => critDamage; }
    public Stat MovementSpeed { get => movementSpeed; }
    
    protected override void Die()
    {
        //game end logics
    }

    private void IncreaseMaxHp(int amount)
    {
        MaxHp += amount;
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

    // private void UpgradeAtk()
    // {
    //     int newValue = Atk.BaseValue + atkUpgradeAmt;
    //     Atk.ModifyBaseValue(newValue);
    //     
    //    // save to file
    // }



    // public void UpgradeDef()
    // {
    //      Stat stat = _playerStats.Def;
    //      int currValue = stat.BaseValue;
    //      int newValue = currValue + defUpgradeAmt;
    //     
    //      _playerStats.Atk.ModifyBaseValue(newValue);


    //     
    //     _playerStats.AddModifier(_playerStats.Def, 5);    -> soul shop upgrade method?
    // }


    //Testing
    //  private void Update()
    // {
    //     if (Input.GetKeyDown("t"))
    //     {
    //         AddModifier(Atk, 3);
    //         //UpgradeAtk();
    //     }
    //
    //     if (Input.GetKeyDown("y"))
    //     {
    //         AddModifier(Atk, 4);
    //     }
    //
    //     if (Input.GetKeyDown("r"))
    //     {
    //         RemoveModifier(Atk, 4);
    //     }
    //
    //     if (Input.GetKeyDown("f"))
    //     {
    //         print("Curr Atk: " + Atk.CurrentValue);
    //     }
    // } 
}