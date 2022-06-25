using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class EnemyStats : CharacterStats, IDamageable
{
    [Header("Ref:")]
    [SerializeField] private HealthBar healthBar;

    [Space]
    [Header("Settings:")]
    [SerializeField] private Stat defense;
    
    public Stat Defense { get => defense; }


    public void Damage(int damageAmount)
    {
        TakeDamage(damageAmount);
        healthBar.UpdateHealthBar(CurrHpPercentage);
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
    
    protected override void Die()
    {
        WaveSpawner.UpdateWaveTotalEnemies();
        gameObject.SetActive(false);
    }
}
