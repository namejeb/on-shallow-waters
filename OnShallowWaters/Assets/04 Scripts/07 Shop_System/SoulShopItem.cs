using UnityEngine;

public class SoulShopItem
{
    public enum ItemType
    {
        HealthPotion,
        SomePotion,
        BooBooPotion,
        ManaPotion,
        AisKosong,
        MiloIce,
        TehTarik,
        KopiOPeng
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return 30;
            case ItemType.SomePotion: return 20;
            case ItemType.BooBooPotion: return 10;
            case ItemType.ManaPotion: return 10;
            case ItemType.AisKosong: return 100;
            case ItemType.MiloIce: return 50;
            case ItemType.TehTarik: return 40;
            case ItemType.KopiOPeng: return 60;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        GameAssets ga = GameAssets.i;

        switch (itemType)
        {
            default:
         
            case ItemType.HealthPotion: return ga.gShop_Hp;
            case ItemType.SomePotion: return ga.gShop_Hp;
            case ItemType.BooBooPotion: return ga.gShop_Hp;
            case ItemType.ManaPotion: return ga.gShop_Hp;
            case ItemType.AisKosong: return ga.gShop_Hp;
            case ItemType.MiloIce: return ga.gShop_Hp;
            case ItemType.TehTarik: return ga.gShop_Hp;
            case ItemType.KopiOPeng: return ga.gShop_Hp;
        }
    }
}