using UnityEngine;
using System;
using TMPro;


[Serializable]
public class Shop : MonoBehaviour
{
    private Transform _container;
    private Transform _shopButtonTemplate;

    private IShopCustomer _shopCustomer;
    
    private void Awake()
    {
        _container = transform.Find("container");
        _shopButtonTemplate = _container.Find("shopButtonTemplate");
        _shopButtonTemplate.gameObject.SetActive(false);

        _shopCustomer = PlayerStats.Instance;
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
    
        
        //click function
        newShopButtonTransform.GetComponent<Button_UI>().ClickEvent (() => TryBuyItem(itemType, currencyType))  ;
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
