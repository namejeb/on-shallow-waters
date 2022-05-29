using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldShop : Shop
{
    private Transform _container;
    private Transform _shopButtonTemplate;

    private void Awake()
    {
        _container = transform.Find("container");
        _shopButtonTemplate = _container.Find("shopButtonTemplate");
        _shopButtonTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateShopButton(null, "s", 1, 0);
        CreateShopButton(null, "a", 1, 1);
        CreateShopButton(null, "d", 1, 2);
    }

    private void CreateShopButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform newShopButtonTransform = Instantiate(_shopButtonTemplate, _container);
        newShopButtonTransform.gameObject.SetActive(true);
        RectTransform newShopButtonRectTransform = newShopButtonTransform.GetComponent<RectTransform>();

        float shopButtonWidth = newShopButtonRectTransform.rect.width;
        newShopButtonRectTransform.anchoredPosition = new Vector2(shopButtonWidth * positionIndex, 0f);
        
        //set ui images, texts
        newShopButtonTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
    
        
        //click function
        newShopButtonTransform.GetComponent<Button_UI>().Func_Click(Test);
    }

    private void Test()
    {
        print("hi");
    }


    //  [SerializeField] private int atkUpgradeAmt = 3;
    //  [SerializeField] private int defUpgradeAmt = 3;
    //
    // private PlayerStats _playerStats;
    //
    //
    // private void Start()
    // {
    //    // _playerStats = PlayerStats.Instance;
    // }
    //
    // public void UpgradeAtk()
    // {
    //      Stat stat = _playerStats.Atk;
    //      int currValue = stat.BaseValue;
    //      int newValue = currValue + atkUpgradeAmt;
    //     
    //      _playerStats.Atk.ModifyBaseValue(newValue);
    //     
    //     save to file
    // }
    //
    // public void UpgradeDef()
    // {
    //      Stat stat = _playerStats.Def;
    //      int currValue = stat.BaseValue;
    //      int newValue = currValue + defUpgradeAmt;
    //     
    //      _playerStats.Atk.ModifyBaseValue(newValue);
    //     
    //     _playerStats.AddModifier(_playerStats.Def, 5);    -> soul shop upgrade method?
    // }
}
