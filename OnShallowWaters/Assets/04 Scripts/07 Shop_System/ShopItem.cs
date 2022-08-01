using UnityEngine;

public class ShopItem 
{
    public enum ItemType {
        HP,
        ATK,
        DEF,
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
            case ItemType.HP:   return 30;
            case ItemType.ATK:  return 20;
            case ItemType.DEF:  return 10;
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
            case ItemType.HP:   return ga.gShop_Hp;
            case ItemType.ATK:  return ga.gShop_Atk;
            case ItemType.DEF:  return ga.gShop_Def;
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

    public static string GetName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HP: return "HP";
            case ItemType.ATK: return "ATK";
            case ItemType.DEF: return "DEF";
            case ItemType.HealthPotion: return "HealthPotion";
            case ItemType.SomePotion: return "SomePotion";
            case ItemType.BooBooPotion: return "BooBooPotion";
            case ItemType.ManaPotion: return "Mana Potion";
            case ItemType.AisKosong: return "AisKosong";
            case ItemType.MiloIce: return "MiloKosong";
            case ItemType.TehTarik: return "TehTarik";
            case ItemType.KopiOPeng: return "KopiOpeng";
        }
    }
}
