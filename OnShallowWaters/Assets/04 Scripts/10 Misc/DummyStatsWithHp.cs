using UnityEngine;
using System;

public class DummyStatsWithHp : DummyStats, IDamageable
{
    [SerializeField] private HealthBar hpBar;

    private bool dead = false;
    
    public static event Action OnAttacked;
    public static event Action OnDeath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitbox_Player"))
        {
            if(OnAttacked != null) OnAttacked.Invoke();
        }
    }
    public new void Damage(int damageAmount)
    {
        base.Damage(damageAmount);
        
        damageAmount = (int) base.GetReceivedDamage(damageAmount);
        TakeDamage(damageAmount);
        float percent = Mathf.Clamp(CurrHpPercentage, 0f, 1f);
        hpBar.UpdateHealthBar(percent);
        
        
        if (currHp <= 0)
        {
            currHp = 0;

            if (dead) return;
            dead = true;
            Die();
            if(OnDeath != null) OnDeath.Invoke();
        }
    }
    
    public void SetHealth(float hp)
    {
        currHp = hp;
    }

    protected override void Die(){
        LeanTween.alpha(transform.parent.gameObject, 0f, 1f);
    }
}
