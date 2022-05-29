
public interface IShopCustomer
{
   public void BoughtItem(ShopItem.ItemType itemType, CurrencyType currencyType);
   public bool TrySpendCurrency(CurrencyType currencyType, int amountToSpend);
}
