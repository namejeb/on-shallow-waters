using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;

public class BoonSelection : MonoBehaviour
{
    [SerializeField] private BoonEffects boonEffects;
    
    
    [Space][Space]
    [SerializeField] private BoonItemsSO boonItemsSo;
    [SerializeField] private float offsetX;
    
    [Space][Space]
    [SerializeField] private Image background;

    private List<BoonItem> boonItemsList = new List<BoonItem>();
    [SerializeField] private List<BoonItemsTimesUsed> _boonItemsTimesUseds = new List<BoonItemsTimesUsed>();
  

    private Transform _container;
    private Transform _boonButtonTemplate;

    private Transform[] _buttons = new Transform[3];
    private BoonItem[] _boonItems = new BoonItem[3];

    public static event Action OnSelectedBoon;

    [Serializable]
    private class BoonItemsTimesUsed
    {
        public BoonItem boonItem;
        public int usageCount = 0;
        private readonly int _maxUsageCount = 1;

        public bool IsLimitReached => usageCount == _maxUsageCount;

        public BoonItemsTimesUsed(BoonItem boonItem)
        {
            this.boonItem = boonItem;
        }
    }
    

    #region Singleton
    public static BoonSelection Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    private void Start()
    {
        _container = transform.Find("container");
        _boonButtonTemplate = _container.Find("boonButtonTemplate");
        _boonButtonTemplate.gameObject.SetActive(false);

        background.enabled = false;
        
        //create 3 buttons at the start
        for (int i = 0; i < 3; i++)
        {
            _buttons[i] = CreateShopButton(i);      
        }
        
        //tracker for each effect's times used
        foreach (BoonItem boonItem in boonItemsSo.boonItems)
        {
            BoonItemsTimesUsed bitu = new BoonItemsTimesUsed(boonItem);
            _boonItemsTimesUseds.Add(bitu);
        }
    }
        
    //Create buttons at start, only change info and onClick functions of these buttons afterwards
    private Transform CreateShopButton(int positionIndex)
    {
        Transform newShopButtonTransform = Instantiate(_boonButtonTemplate, _container);
        RectTransform newShopButtonRectTransform = newShopButtonTransform.GetComponent<RectTransform>();

        float shopButtonWidth = newShopButtonRectTransform.rect.width;
        newShopButtonRectTransform.anchoredPosition = new Vector2(shopButtonWidth * positionIndex * offsetX, newShopButtonRectTransform.anchoredPosition.y);
        
        return newShopButtonTransform;
    }
    private void ActivateBoonEffect(int effectIndex)
    {
        //boonEffects.UpgradeAtk();
        switch (effectIndex)
        {
            case 0: boonEffects.UpgradeAtkPercent();         break;
            case 1: boonEffects.UpgradeAtkSpd();      break;
            case 2: boonEffects.UpgradeCritChance();  break;
            case 3: boonEffects.UpgradeCritDamage();  break;
        }

        //increment usage
        BoonItemsTimesUsed bitu = _boonItemsTimesUseds.Find(b => b.boonItem.id == effectIndex);
        
        if(!bitu.IsLimitReached)
            bitu.usageCount++;
        else
        {    
            //remove from pool;
            boonItemsList.Remove(boonItemsList.Find(bI => bI.id == effectIndex));
        }
    }

    
    //List of boons to randomize from
    private void PopulateBoonItemsPool()
    {
        //set up pool
        boonItemsList.Clear();
        foreach (BoonItem boonItem in boonItemsSo.boonItems)
        {
            boonItemsList.Add(boonItem);
        }
    }
    
    private void RandomiseBoonItems()
    {
        BoonItem[] boonItems = new BoonItem[3];
        for (int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, boonItemsList.Count);
            BoonItem bI = boonItemsList[index];

            boonItems[i] = bI;
            boonItemsList.Remove(bI);  //remove from pool to avoid duplicates
        }
        _boonItems = boonItems;
    }
    
    private void InitButtons()
    {
        SetBoonButtonsActive(true);
        
        for (int i = 0; i < 3; i++)
        {
            BoonItem boonItem = _boonItems[i];

            //Set info
            _buttons[i].Find("titleText").GetComponent<TextMeshProUGUI>().SetText(boonItem.title);
            _buttons[i].Find("descText").GetComponent<TextMeshProUGUI>().SetText(boonItem.description);
            
            //Set onClick function
            Button_UI buttonUI = _buttons[i].GetComponent<Button_UI>();
            
            //Remove previous clickEvents
            buttonUI.ClearAllListeners();          
            
            //Add new clickEvent
            buttonUI.ClickEvent(() => ActivateBoonEffect(boonItem.id) );
            buttonUI.ClickEvent(() => CloseBoonSelection());
        }
    }
    public void RollBoons()
    {
        background.enabled = true;
        PopulateBoonItemsPool();
        RandomiseBoonItems();
        InitButtons();
    }
    
    private void SetBoonButtonsActive(bool status)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].gameObject.SetActive(status);
        }
    }
    
    private void CloseBoonSelection()
    {
        SetBoonButtonsActive(false);
        background.enabled = false;
        
        //open invi door
        if(OnSelectedBoon != null) OnSelectedBoon.Invoke();
    }

}
