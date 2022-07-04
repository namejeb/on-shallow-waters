using _04_Scripts._05_Enemies_Bosses;

public class BreakableProp : EarnCurrencyItems, IDamageable
{
    public void Damage(int damageAmount)
    {
        Break();
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }

    private void Break()
    {
        //play sound
        
        EarnGold(minMaxAmount.x, minMaxAmount.y);
        gameObject.SetActive(false);
    }
}
