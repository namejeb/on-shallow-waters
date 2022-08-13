using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;
using System;

public class EnemyStats : CharacterStats, IDamageable
{
    [Header("Ref:")]
    [SerializeField] private HealthBar healthBar;
    private DropSouls _dropSouls;
    private EnemiesCore _enemiesCore;
    
    [Space]
    [Header("Settings:")]
    [SerializeField] private Stat defense;
    private float _defPercent = 1f;
    
    //  private float _movementSpeed = 1f;
    //  private float _attackSpeed = 1f;
    
    [SerializeField] private new Collider collider;
    public Stat Defense { get => defense; }

    public bool isDead = false;
    public bool isHit = false;
    public Animator anim1;

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
        collider.enabled = true;
    }
    
    public void Damage(int damageAmount)
    {
        if (_enemiesCore.armourType && !_enemiesCore.shieldDestroy) {
            _enemiesCore.ShieldBar(damageAmount);
        } else {
            anim1.SetTrigger("isHit");
            anim1.ResetTrigger("isAttack1");
            anim1.ResetTrigger("isAttack2");

            damageAmount = (int) ReceiveIncomingDamage(damageAmount);
            TakeDamage(damageAmount);
        } 

        healthBar.UpdateHealthBar(CurrHpPercentage);
    }

    public float GetReceivedDamage(float incomingDamage)
    {
        return ReceiveIncomingDamage(incomingDamage);
    }

    private float ReceiveIncomingDamage(float incomingDamage)
    {
        incomingDamage *= (100 / (25 + (_defPercent * defense.CurrentValue)));
        return incomingDamage;
    }

    public float LostHP()
    {
        return MaxHp - currHp;
    }

    protected override void Die()
    {
        if (isDead) return;
        isDead = true;
        anim1.SetTrigger("isDead");
        
        _dropSouls.Drop();

        WaveSpawner.Instance.UpdateWaveTotalEnemies();
        if (WaveSpawner.Instance.IsLastEnemy)
        {
            SpawnBoonTrigger.Instance.SpawnAtPosition(transform.position);
        }

        collider.enabled = false;
        
        Invoke(nameof(DisableSelf), 1f);
    }

    //change to call in animation?
    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
