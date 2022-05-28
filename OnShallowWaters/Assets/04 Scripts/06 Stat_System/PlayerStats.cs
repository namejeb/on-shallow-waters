
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public static PlayerStats Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            AddModifier(atk, 3);
        }

        if (Input.GetKeyDown("r"))
        {
            RemoveModifier(atk, 4);
        }

        if (Input.GetKeyDown("f"))
        {
            print("Curr Atk: " + Atk.CurrentValue);
        }
    }
    
    public override void Die()
    {
        //game end logics
    }

    private void IncreaseMaxHp(int amount)
    {
        hp += amount;
    }
}