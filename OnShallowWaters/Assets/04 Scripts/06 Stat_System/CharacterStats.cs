using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private float maxHp = 5;
    protected float currHp;
    
    [Space][Space]
    [Header("Stats:")]
    [SerializeField] private Stat atk;

    public float MaxHp
    {
        get => maxHp; 
        protected set => maxHp = value;
    }
    public Stat Atk { get => atk; }
    
    
    public float CurrHpPercentage
    {
        get => currHp / MaxHp;
    }

    private void OnDisable()
    {
        if(!isPlayer)
            currHp = maxHp;
    }
    
    protected void Awake()
    {
        currHp = maxHp;
    }
    
    protected void TakeDamage(float dmg)
    {
        currHp -= dmg;
        
        if (currHp <= 0f)
        {
            currHp = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        // implement different die functionalities      
    }

    public void AddModifier(Stat statToModify, float modifier)
    {
        statToModify.AddModifier(modifier);
    }

    public void RemoveModifier(Stat statToModify, float modifier)
    {
        statToModify.RemoveModifier(modifier);
    }
}
