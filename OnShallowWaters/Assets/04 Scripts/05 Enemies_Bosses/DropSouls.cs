
public class DropSouls : EarnCurrencyItems
{
    private void OnDisable()
    {
        EarnSoul(minMaxAmount.x, minMaxAmount.y);
    }
}
