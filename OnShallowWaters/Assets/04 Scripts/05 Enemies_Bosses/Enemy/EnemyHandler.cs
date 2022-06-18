using _04_Scripts._05_Enemies_Bosses.Enemy;
using UnityEngine;

public class EnemyHandler : CharacterStats, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    
    
    public void Damage(int damageAmount)
    {
        TakeDamage(damageAmount);
        healthBar.UpdateHealthBar(CurrHpPercentage);
    }

    protected override void Die()
    {
        WaveSpawner.UpdateWaveTotalEnemies();
        gameObject.SetActive(false);
    }
}
