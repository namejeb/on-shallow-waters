
public class GoldShop : Shop
{
    private void Start()
    {
        CreateShopButton(ShopItem.ItemType.HP, CurrencyType.GOLD, ShopItem.GetSprite(ShopItem.ItemType.HP), "hp", 1, 0);
        CreateShopButton(ShopItem.ItemType.ATK, CurrencyType.GOLD, ShopItem.GetSprite(ShopItem.ItemType.ATK), "atk", 1, 1);
        CreateShopButton(ShopItem.ItemType.DEF, CurrencyType.SOULS,ShopItem.GetSprite(ShopItem.ItemType.DEF), "def", 1, 2);
        
        //Hide Shop when start
    }
}
