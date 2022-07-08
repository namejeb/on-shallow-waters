using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;

public class BoonSelection : MonoBehaviour
{
    [Serializable]
    private class BoonItemsTimesUsed
    {
        public BoonItem boonItem;
        public int usageCount = 0;
        private readonly int _maxUsageCount;

        public bool IsLimitReached => usageCount == _maxUsageCount;

        public BoonItemsTimesUsed(BoonItem boonItem)
        {
            this.boonItem = boonItem;
            this._maxUsageCount = boonItem.maxUsageCount;
        }
    }
    
    [SerializeField] private BoonEffects boonEffects;
    
    [Space][Space]
    [SerializeField] private BoonItemsSO boonItemsSo;
    [SerializeField] private float offsetX;
    
    [Space][Space]
    [SerializeField] private Image background;

    private List<BoonItem> boonItemsList = new List<BoonItem>();
    private List<BoonItemsTimesUsed> _boonItemsTimesUsed = new List<BoonItemsTimesUsed>();
  

    private Transform _container;
    private Transform _boonButtonTemplate;

    private Transform[] _buttons = new Transform[3];
    private BoonItem[] _boonItems = new BoonItem[3];

    private BoonEffects _boonEffects;

    public static event Action OnSelectedBoon;


    #region Singleton
    public static BoonSelection Instance;

    private void Awake()
    {
        Instance = this;
        boonEffects = GetComponent<BoonEffects>();
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
            _boonItemsTimesUsed.Add(bitu);
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
    private void ActivateBoonEffect(int boonItemId)
    {
        boonEffects.HandleEffectActivation(boonItemId);
        
        //increment usage
        BoonItemsTimesUsed bitu = _boonItemsTimesUsed.Find(b => b.boonItem.id == boonItemId);
        if (!bitu.IsLimitReached) bitu.usageCount++;
    }

    
    //List of boons to randomize from
    private void PopulateBoonItemsPool()
    {
        // set up pool
        boonItemsList.Clear();
         foreach (BoonItem boonItem in boonItemsSo.boonItems)
         {
             boonItemsList.Add(boonItem);
         }
                 
         // remove effects that have reached highest increase amount from pool
         for(int i = 0; i < _boonItemsTimesUsed.Count; i++)
         {
             if (_boonItemsTimesUsed[i].IsLimitReached)
             {
                 BoonItem bI = _boonItemsTimesUsed[i].boonItem;
                 boonItemsList.Remove(bI);
             }
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

            SetButtonInfo(_buttons[i], boonItem);
            
            //Set onClick function
            Button_UI buttonUI = _buttons[i].GetComponent<Button_UI>();
            
            //Remove previous clickEvents
            buttonUI.ClearAllListeners();          
            
            //Add new clickEvent
            buttonUI.ClickEvent(() => ActivateBoonEffect(boonItem.id) );
            buttonUI.ClickEvent(() => CloseBoonSelection());
        }
    }

    private void SetButtonInfo(Transform buttonTransform, BoonItem boonItem)
    {
        //Set info
        buttonTransform.Find("titleText").GetComponent<TextMeshProUGUI>().SetText(boonItem.title);
            
        float effectAmount = 0f;
        
        // if (boonItem.id == 0)
        //     effectAmount = boonEffects.GetMaxHpIncreaseAmount();
        // else
        //     effectAmount =  boonEffects.GetStatIncreaseAmounts(boonItem.id);

        effectAmount =  boonEffects.GetStatIncreaseAmounts(boonItem.id);
        
        if(boonItem.isPercentage)
            buttonTransform.Find("descText").GetComponent<TextMeshProUGUI>().SetText($"{boonItem.description} +{effectAmount * 100}%");
        else
            buttonTransform.Find("descText").GetComponent<TextMeshProUGUI>().SetText($"{boonItem.description} {effectAmount}");
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
