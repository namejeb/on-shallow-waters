using UnityEngine;


public class BoonEffectsManager : MonoBehaviour
{
    public static BoonsList BoonsList { get; private set; }

    private void Awake()
    {
        BoonsList = GetComponent<BoonsList>();
    }
    
    public void MaxHp()
    {
        BM_MaxHp b = GetComponent<BM_MaxHp>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
        print(PlayerHandler.Instance.PlayerStats.MaxHp);
    }

    public void Defense()
    {
        BM_Defense b = GetComponent<BM_Defense>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
    }

    public void MovementSpeed()
    {
        BM_MovementSpeed b = GetComponent<BM_MovementSpeed>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
    }

    public void LowHpDmgReduction()
    {
        BM_LowHpDmgReduction b = GetComponent<BM_LowHpDmgReduction>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
    }
    

    
    public void ArmorDmgBonus()
    {
        BM_ArmorDmgBonus b = GetComponent<BM_ArmorDmgBonus>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
        print(EnemiesCore.shieldDmgBonus);
    }
    public void DmgWhenArmorBreak()
    {
        BM_DmgWhenArmorBreak b = GetComponent<BM_DmgWhenArmorBreak>();
        HandleEnablingAndUpgrading(b);
    }

    public void IncreaseDmgIncreaseSingleEnemy()
    {
        BA_SingleEnemyDmgBonus b = GetComponent<BA_SingleEnemyDmgBonus>();
        HandleEnablingAndUpgrading(b);
    }
    
    public void FirstTimeDmgBonus()
    {
        BA_FirstTimeDmgBonus b = GetComponent<BA_FirstTimeDmgBonus>();
        HandleEnablingAndUpgrading(b);  
    }
    

    
    
    public void UpgradeAtkPercent()
    {
        BM_AtkPercent b = GetComponent<BM_AtkPercent>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
        print(PlayerHandler.Instance.PlayerStats.AtkPercent);
    }
    public void UpgradeAtkSpeed()
    {
        BM_AtkSpeed b = GetComponent<BM_AtkSpeed>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
       // print(PlayerHandler.Instance.PlayerStats.AtkSpeed);
    }

    public void UpgradeCritChance()
    {
        BM_CritChance b = GetComponent<BM_CritChance>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
       // print(PlayerHandler.Instance.PlayerStats.CritChance);
    }
    
    public void UpgradeCritDamage()
    {
        BM_CritDamage b = GetComponent<BM_CritDamage>();
        HandleEnablingAndUpgrading(b);
        b.ApplyEffect();
      //  print(PlayerHandler.Instance.PlayerStats.CritDamage);
    }




    private void HandleEnablingAndUpgrading(Boon b){
        // if(b.Activated) { b.Upgrade(); }
        // else { b.Activated = true; }

        if (!b.Activated) { b.Activated = true; }
        b.Upgrade();
    }
}
