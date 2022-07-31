using UnityEngine;
using System;
using TMPro;
using Random = UnityEngine.Random;
using System.Collections.Generic;


[Serializable]
public class Shop : MonoBehaviour
{
    private Transform _container;
    private Transform _shopButtonTemplate;

    private IShopCustomer _shopCustomer;

    private List<ShopItem.ItemType> Values = new List<ShopItem.ItemType>(); 
    private void Awake()
    {
        _container = transform.Find("container");
        _shopButtonTemplate = _container.Find("ShopButtonTemplate");
        _shopButtonTemplate.gameObject.SetActive(false);

      
    }

    private void Start()
    {
        _shopCustomer = PlayerHandler.Instance.PlayerStats;
        CreateButton();
        Hide();
    }

    private void CreateButton()
    {
        for (int i = 0; i < 3; i++)
        {

            ShopItem.ItemType type = RandomItems();
            CreateShopButton(type, CurrencyType.SOULS, ShopItem.GetSprite(type), ShopItem.GetName(type), ShopItem.GetCost(type), i*3);
            
        }
    }
  
    public ShopItem.ItemType RandomItems()
    {
        ShopItem.ItemType RandomItem = (ShopItem.ItemType)Random.Range(0, 11);
        while (Values.Contains(RandomItem))
        {
            RandomItem = (ShopItem.ItemType)Random.Range(0, 11);
        }
        return RandomItem;
    }


    protected void CreateShopButton(ShopItem.ItemType itemType, CurrencyType currencyType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform newShopButtonTransform = Instantiate(_shopButtonTemplate, _container);
        newShopButtonTransform.gameObject.SetActive(true);
        RectTransform newShopButtonRectTransform = newShopButtonTransform.GetComponent<RectTransform>();

        float shopButtonWidth = newShopButtonRectTransform.rect.width;
        newShopButtonRectTransform.anchoredPosition = new Vector2(shopButtonWidth * positionIndex, 0f);
        
        //set ui images, texts
        newShopButtonTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        newShopButtonTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        //click function
       //newShopButtonTransform.GetComponent<Button_UI>().ClickEvent (() => TryBuyItem(itemType, currencyType))  ;
    }
    
    private void TryBuyItem(ShopItem.ItemType itemType, CurrencyType currencyType)
    {
        if (_shopCustomer.TrySpendCurrency(currencyType, ShopItem.GetCost(itemType)))
        {
            _shopCustomer.BoughtItem(itemType, currencyType);
        }
        else
        {
            //implement tooltip or ui change
            print($"cannot afford {itemType}!");
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        _shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
