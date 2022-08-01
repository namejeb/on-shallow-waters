using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using System;


public class PlayerStats : CharacterStats, IShopCustomer, IDamageable
{
    [Space][Space]
    [Header("System Refs:")]
    [SerializeField] private PlayerHealthBar playerHealthBar;
    [SerializeField] private BM_LowHpDmgReduction bmLowHpDmgReduction;
   
    // Animation
    private Animator _anim;
    
    // Death
    //[SerializeField] private Transform loseScreenContainer;    
    public static event Action OnPlayerDeath;

    [Space][Space]
    [Header("Stats:")]
    [SerializeField] private Stat movementSpeed;
    [SerializeField] private Stat defense;
    
    private float _atkPercent = 1f;
    private float _mvmntSpdMutliplier = 1f;
    private float _defMutliplier = 1f;
    
    private float _damageReduction = 1f;

    [SerializeField] private float _critChance = .3f;  
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
    

    private new void Awake()
    {
        currHp = MaxHp;
        playerHealthBar.SetMaxHealth(MaxHp);

        // Animation
        _anim = GetComponent<Animator>();
    }
    
    protected override void Die()
    {
        //game end logics
        _anim.Play("Death");
        
        // disable controls
        GetComponent<DashNAttack>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        
        // Enable lose screen
        if(OnPlayerDeath != null) OnPlayerDeath.Invoke();
    }

    // Called in animation - "Death"
    public void PlayDeathFloat()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0, 7.5f, 0f);
    }
    
    public void IncreaseAtkPercent(float multiplierToSet)
    {
        _atkPercent = multiplierToSet;
    }
    
    public void IncreaseDamageReduction(float multiplierToSet)
    {
        _damageReduction = multiplierToSet;
    }

    public void IncreaseCritChance(float multiplier)
    {
        _critChance *= multiplier;
    }
    
    public void IncreaseCritDmg(float multiplierToSet)
    {
        _critDamage *= multiplierToSet;
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
            if (currHp < MaxHp)
            {
                currHp += regenHp;
            }
            playerHealthBar.SetHealth(currHp);
            Debug.Log(currHp);
        }

    }

    [Button]
    private void TempDamage()
    {
        _anim.Play("Get hit");
        float damageAmount = 20;
        
        int effectiveDmg = (int) ReceiveIncomingDmg(damageAmount);
        TakeDamage(effectiveDmg);
        playerHealthBar.SetHealth(currHp);
    }
    
    public void Damage(int damageAmount)
    {
        _anim.Play("Get hit");
        
        int effectiveDmg = (int) ReceiveIncomingDmg(damageAmount);
        TakeDamage(effectiveDmg);
        playerHealthBar.SetHealth(currHp);
    }


    private float ReceiveIncomingDmg(float incomingDamage)
    {
        incomingDamage *= (100 / (25 + (DefMultiplier * (Defense.CurrentValue + 0))) * DamageReduction);

        if (bmLowHpDmgReduction.Activated)
        {
            if (CurrHpPercentage < bmLowHpDmgReduction.DmgReductionActivationThreshold)
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
    //
    // void Update()
    // {
    //     if (Input.GetKeyDown("r"))
    //     {
    //         RemoveModifier(Atk, 25.9f);
    //     }
    //
    //     if (Input.GetKeyDown("a"))
    //     {
    //         AddModifier(Atk , 25.9f);
    //     }
    //
    // }

    public void DamageTest(int damage)
    {
        currHp -= damage;
        playerHealthBar.SetHealth(currHp);
    }
}