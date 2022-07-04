using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : Shop
{
    private void Start()
    {
        CreateShopButton(ShopItem.ItemType.AisKosong, CurrencyType.SOULS, ShopItem.GetSprite(ShopItem.ItemType.HP), "Ais Kosong", 100, 0);
        CreateShopButton(ShopItem.ItemType.MiloIce, CurrencyType.SOULS, ShopItem.GetSprite(ShopItem.ItemType.HP), "MILO ICE", 20, 1);
        CreateShopButton(ShopItem.ItemType.KopiOPeng, CurrencyType.SOULS, ShopItem.GetSprite(ShopItem.ItemType.HP), "KOPI O PENG", 30, 2);

        //Hide Shop when start
    }
}
