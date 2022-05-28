
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
}