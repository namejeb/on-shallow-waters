using _04_Scripts._05_Enemies_Bosses;
using _04_Scripts._05_Enemies_Bosses.Enemy;
using UnityEngine;

public class EnemyHandler : CharacterStats, IDamageable
{
    //temp damage to test WaveSpawner, will remove
    [SerializeField] private HealthBar healthBar;
    
    //temp damage to test WaveSpawner, will remove
    public void Damage(int damageAmount)
    {
        TakeDamage(damageAmount);
        healthBar.UpdateHealthBar(CurrHpPercentage);
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }

    //temp damage to test WaveSpawner, will remove
    protected override void Die()
    {
        WaveSpawner.UpdateWaveTotalEnemies();
        print("died");
        gameObject.SetActive(false);
    }
}
