using UnityEngine;

public class EarnCurrencyItems : EarnPickups
{
    // Gives the currencies player based on specified minMax amounts
    
    protected void EarnGold(int amt)
    {
        CurrencySystem.AddCurrency(CurrencyType.GOLD, amt);
    }

    protected void EarnGold()
    {
        CurrencySystem.AddCurrency(CurrencyType.GOLD, minMaxAmount.x, minMaxAmount.y);
    }

    protected void EarnSoul(int amt)
    {
        CurrencySystem.AddCurrency(CurrencyType.SOULS, amt);
    }

    protected void EarnSoul()
    {
        CurrencySystem.AddCurrency(CurrencyType.SOULS, minMaxAmount.x, minMaxAmount.y);
    }
}
