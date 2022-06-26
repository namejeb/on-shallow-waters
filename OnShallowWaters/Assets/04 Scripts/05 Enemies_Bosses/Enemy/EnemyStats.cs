using _04_Scripts._05_Enemies_Bosses;
using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEngine;

public class EnemyStats : CharacterStats, IDamageable
{
    [Header("Ref:")]
    [SerializeField] private HealthBar healthBar;
    private DropSouls _dropSouls;

    private EnemiesCore _enemiesCore;
    
    [Space]
    [Header("Settings:")]
    [SerializeField] private Stat defense;
    
    public Stat Defense { get => defense; }
    
    private new void Awake()
    {
        base.Awake();
        
        _dropSouls = GetComponent<DropSouls>();
        _enemiesCore = GetComponent<EnemiesCore>();
    }
    public void Damage(int damageAmount)
    {
        if (_enemiesCore.armourType) _enemiesCore.ShieldBar(damageAmount);
        else TakeDamage(damageAmount);

        healthBar.UpdateHealthBar(CurrHpPercentage);
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
    
    protected override void Die()
    {
        _dropSouls.Drop();
        WaveSpawner.UpdateWaveTotalEnemies();

        if (WaveSpawner.IsLastEnemy)
        {
            SpawnBoonTrigger.Instance.SpawnAtPosition(transform.position);
        }
        
        gameObject.SetActive(false);
    }
    
}
