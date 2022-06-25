using _04_Scripts._05_Enemies_Bosses;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : CharacterStats, IDamageable
{
    [Header("Ref:")]
    [SerializeField] private HealthBar healthBar;
    private DropSouls _dropSouls;
    
    [Space]
    [Header("Settings:")]
    [SerializeField] private Stat defense;
    
    public Stat Defense { get => defense; }

    private void OnDisable()
    {
        LeanTween.reset();
    }
    
    private void Awake()
    {
        _dropSouls = GetComponent<DropSouls>();
    }
    public void Damage(int damageAmount)
    {
        TakeDamage(damageAmount);
        print(damageAmount);
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
        gameObject.SetActive(false);
    }
    
}
