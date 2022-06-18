using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int hp = 5;
    protected int _currHp;
    
    [Space][Space]
    [Header("Stats:")]
    [SerializeField] private Stat atk;
    
    public int Hp { get; protected set ; }
    public Stat Atk { get => atk; }
    
    
    public float CurrHpPercentage
    {
        get => (float) _currHp / hp;
    }
    
    private void Awake()
    {
        _currHp = hp;
    }
    
    public void TakeDamage(int dmg)
    {
        if (_currHp - dmg < 0) return;  //prevent dying again
        _currHp -= dmg;
        
        if (_currHp <= 0)
        {
            _currHp = 0;
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
