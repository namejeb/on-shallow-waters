using System.Collections;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        //player death logic
        print("player died");
    }

    private void IncreaseMaxHp(int amount)
    {
        Hp += amount;
    }
}