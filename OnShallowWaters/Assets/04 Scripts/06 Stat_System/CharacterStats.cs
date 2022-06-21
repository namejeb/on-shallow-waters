using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int maxHp = 5;
    protected int currHp;
    
    [Space][Space]
    [Header("Stats:")]
    [SerializeField] private Stat atk;

    public int MaxHp
    {
        get => maxHp; 
        protected set => maxHp = value;
    }
    public Stat Atk { get => atk; }
    
    
    public float CurrHpPercentage
    {
        get => (float) currHp / MaxHp;
    }

    private void OnDisable()
    {
        currHp = maxHp;
    }
    
    protected void Awake()
    {
        currHp = maxHp;
    }
    
    protected void TakeDamage(int dmg)
    {
        if (currHp - dmg < 0) return;  //prevent dying again
        currHp -= dmg;
        
        if (currHp <= 0)
        {
            currHp = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        // implement different die functionalities      
    }

    public void AddModifier(Stat statToModify, int modifier)
    {
        statToModify.AddModifier(modifier);
    }

    public void RemoveModifier(Stat statToModify, int modifier)
    {
        statToModify.RemoveModifier(modifier);
    }
}
