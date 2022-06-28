using _04_Scripts._05_Enemies_Bosses;
using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEngine;
using System;

public class EnemyStats : CharacterStats, IDamageable
{
    [Header("Ref:")]
    [SerializeField] private HealthBar healthBar;
    private DropSouls _dropSouls;

    private EnemiesCore _enemiesCore;

    public static event Action OnEnemyDeath;
    
    [Space]
    [Header("Settings:")]
    [SerializeField] private Stat defense;
    
    public Stat Defense { get => defense; }

    private bool isDead = false;

    private new void Awake()
    {
        base.Awake();
        
        _dropSouls = GetComponent<DropSouls>();
        _enemiesCore = GetComponent<EnemiesCore>();
  
        
    }

    private void OnEnable()
    {
        healthBar.UpdateHealthBar(CurrHpPercentage);
        isDead = false;
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
        if (isDead) return;
        isDead = true;
        
        _dropSouls.Drop();
        
        // TestWave testWave = TestWave.Instance;
        // ;
        // testWave.UpdateEnemyCount();
        // print(name);
        
       // if(OnEnemyDeath != null) OnEnemyDeath.Invoke();
       
       
        WaveSpawner.Instance.UpdateWaveTotalEnemies();
        if (WaveSpawner.Instance.IsLastEnemy)
        {
            SpawnBoonTrigger.Instance.SpawnAtPosition(transform.position);
        }

        Invoke(nameof(DisableSelf), 1f);
    }

    //change to call in animation?
    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
    
}