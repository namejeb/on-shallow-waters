
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        //game end logics
    }

    private void IncreaseMaxHp(int amount)
    {
        Hp += amount;
    }

    private void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            AddModifier(Atk, 3);
        }

        if (Input.GetKeyDown("r"))
        {
            RemoveModifier(Atk, 4);
        }
    }
}