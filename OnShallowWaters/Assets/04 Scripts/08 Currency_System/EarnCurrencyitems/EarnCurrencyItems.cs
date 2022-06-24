using UnityEngine;

public class EarnCurrencyItems : MonoBehaviour
{
    [SerializeField] protected Vector2Int minMaxAmount;
    
    protected void EarnGold(int amt)
    {
        CurrencySystem.AddCurrency(CurrencyType.GOLD, amt);
    }

    protected void EarnGold(int minAmt, int maxAmt)
    {
        CurrencySystem.AddCurrency(CurrencyType.GOLD, minAmt, maxAmt);
    }

    protected void EarnSoul(int amt)
    {
        CurrencySystem.AddCurrency(CurrencyType.SOULS, amt);
    }

    protected void EarnSoul(int minAmt, int maxAmt)
    {
        CurrencySystem.AddCurrency(CurrencyType.SOULS, minAmt, maxAmt);
    }
}
