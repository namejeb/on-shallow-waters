using System;
using UnityEngine;


public class BoonEffects : MonoBehaviour {
    
    private BoonEffectsManager boonEffectsManager;
    
    [Space] public int boonToUse;

    private void Start()
    {
        boonEffectsManager = PlayerHandler.Instance.BoonEffectsManager;
    }

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
