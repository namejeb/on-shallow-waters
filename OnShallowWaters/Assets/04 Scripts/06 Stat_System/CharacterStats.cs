using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int Hp ;
    protected int _currHp;
    
    [SerializeField] protected Stat Atk;
    [SerializeField] protected Stat Def;
    

    public float CurrHpPercentage
    {
        get => (float) _currHp / Hp;
    }
    
    private void Awake()
    {
        _currHp = Hp;
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

    public virtual void Die()
    {
        //implement different die functionalities      
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
