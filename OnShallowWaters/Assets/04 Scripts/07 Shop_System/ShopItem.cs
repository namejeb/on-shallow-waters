using UnityEngine;

public class ShopItem 
{
    public enum ItemType {
        HP,
        ATK,
        DEF
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HP:   return 30;
            case ItemType.ATK:  return 20;
            case ItemType.DEF:  return 10;
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
        }
    }
}
