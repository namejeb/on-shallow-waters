using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEngine;

public class BoonDamageModifiers : MonoBehaviour
{
   private bool _dmgIncreaseSingleEnemyActivated = false;
   private bool _firstTimeDmgBonusActivated = false;
   private bool _dmgWhenShieldBreakActivated = false;

   private float _dmgIncreaseSingleEnemyModifier = 1f; 
   private float _firstTimeDmgBonusModifier = 1f;
   private int _dmgWhenShieldBreakModifier;
 

   public bool DmgWhenShieldBreakActivated { get => _dmgWhenShieldBreakActivated; }
   
   public float ApplyModifiers(float outgoingDmg, EnemyHandler enemyHandler)
   {
      if (_dmgIncreaseSingleEnemyActivated)
      {
         if (WaveSpawner.GetCurrWaveTotalEnemies() == 1)
         {
            outgoingDmg = ApplyDamageIncreaseToSingleEnemy(outgoingDmg);
         }
      }

      if (_firstTimeDmgBonusActivated)
      {
         if (enemyHandler.EnemyStats.CurrHpPercentage >= .999f)
         {
            outgoingDmg = ApplyFirstTimeDamageBonus(outgoingDmg);
         }
      }
      return outgoingDmg;
   }
   
   private float ApplyDamageIncreaseToSingleEnemy(float outgoingDmg)
   {
      return outgoingDmg * _dmgIncreaseSingleEnemyModifier;
   }

   private float ApplyFirstTimeDamageBonus(float outgoingDmg)
   {
      return outgoingDmg * _firstTimeDmgBonusModifier;
   }

   public void ApplyShieldBreakDamage(EnemyHandler enemyHandler)
   {
      if (enemyHandler.EnemiesCore.shieldDestroy)
      {
         enemyHandler.EnemyStats.Damage( _dmgWhenShieldBreakModifier );
      }
   }

   public void EnableSingleEnemyDmgIncrease(float effectAmount)
   {
      _dmgIncreaseSingleEnemyActivated = true;
      _dmgIncreaseSingleEnemyModifier = effectAmount;
   }

   public void EnableFirstTimeDmgBonus(float effectAmount)
   {
      _firstTimeDmgBonusActivated = true;
      _firstTimeDmgBonusModifier = effectAmount;
   }

   public void EnableDmgWhenShieldBreak(int effectAmount)
   {
      _dmgWhenShieldBreakActivated = true;
      _dmgWhenShieldBreakModifier = effectAmount;
   }

   public void EnableExtraShieldDmg(float effectAmount)
   {
      EnemiesCore.shieldDmgBonus = effectAmount;
   }
}
