using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;

[Serializable]
public class BoonItemsTimesUsed
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

public class BoonSelection : MonoBehaviour
{
    [SerializeField] private BoonEffects boonEffects;
    [SerializeField] private BoonsList boonList;
    
    
    [Space][Space]
    [SerializeField] private BoonItemsSO boonItemsSo;
    [SerializeField] private float offsetX;
    
    [Space][Space]
    [SerializeField] private Image background;

    private List<BoonItem> boonItemsList = new List<BoonItem>();
    private List<BoonItemsTimesUsed> _boonItemsTimesUsed = new List<BoonItemsTimesUsed>();
    public List<BoonItemsTimesUsed> chosenBoonsItems = new List<BoonItemsTimesUsed>();
    
    private Transform _containerButtons; 
    private Transform _boonButtonTemplate;

    private Transform[] _buttons = new Transform[3];
    private BoonItem[] _randomisedBoonItems = new BoonItem[3];

    private BoonEffects _boonEffects;
    
    public static event Action OnSelectedBoon;
    public static event Action<List<BoonItemsTimesUsed>, bool> OnListChanged;


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
        SetupButtons();
        
        chosenBoonsItems.Clear();
        
        //tracker for each effect's times used
        foreach (BoonItem boonItem in boonItemsSo.boonItems)
        {
            BoonItemsTimesUsed bitu = new BoonItemsTimesUsed(boonItem);
            _boonItemsTimesUsed.Add(bitu);
        }
    }

    private void SetupButtons()
    {
        _containerButtons = transform.Find("container_buttons");
        _boonButtonTemplate = _containerButtons.Find("boonButtonTemplate");
        _boonButtonTemplate.gameObject.SetActive(false);

        background.enabled = false;
        
        //create 3 buttons at the start
        for (int i = 0; i < 3; i++)
        {
            _buttons[i] = CreateShopButton(i);      
        }
    }

    //Create buttons at start, only change info and onClick functions of these buttons afterwards
    private Transform CreateShopButton(int positionIndex)
    {
        Transform newShopButtonTransform = Instantiate(_boonButtonTemplate, _containerButtons);
        RectTransform newShopButtonRect = newShopButtonTransform.GetComponent<RectTransform>();

        float shopButtonWidth = newShopButtonRect.rect.width;
        newShopButtonRect.anchoredPosition = new Vector2(shopButtonWidth * positionIndex * offsetX, newShopButtonRect.anchoredPosition.y);
        
        return newShopButtonTransform;
    }
    private void ActivateBoonEffect(int boonItemId)
    {
        boonEffects.HandleEffectActivation(boonItemId);
        
        //increment usage
        BoonItemsTimesUsed bitu = _boonItemsTimesUsed.Find(b => b.boonItem.id == boonItemId);
        if (!bitu.IsLimitReached) bitu.usageCount++;

        //add to list
        if (!chosenBoonsItems.Contains(bitu))
        {
            chosenBoonsItems.Add(bitu);
            if(OnListChanged != null) OnListChanged.Invoke(chosenBoonsItems, true);
           
        }
        //replace element
        else
        {
           int index = chosenBoonsItems.FindIndex(b => b.boonItem.id == boonItemId);                            
           chosenBoonsItems[index] = bitu;
           if(OnListChanged != null) OnListChanged.Invoke(chosenBoonsItems, false);
        }
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
        _randomisedBoonItems = boonItems;
    }
    
    private void InitButtons()
    {
        SetBoonButtonsActive(true);
  
        for (int i = 0; i < 3; i++)
        {
            BoonItem boonItem = _randomisedBoonItems[i];
   
            SetBoonInfo(_buttons[i], boonItem);
            
            //Set onClick function
            Button_UI buttonUI = _buttons[i].GetComponent<Button_UI>();
            
            //Remove previous clickEvents
            buttonUI.ClearAllListeners();          
            
            //Add new clickEvent
            buttonUI.ClickEvent(() => ActivateBoonEffect(boonItem.id) );
            buttonUI.ClickEvent(() => CloseBoonSelection());
        }
    }

    public void SetBoonInfo(Transform boonTransform, BoonItem boonItem, bool isUpgrade = true)
    {
        //Set info
        boonTransform.Find("titleText").GetComponent<TextMeshProUGUI>().SetText(boonItem.title);
        boonTransform.Find("icon").GetComponent<Image>().sprite = boonItem.icon;
            
        float effectAmount = 0f;
        Boon boon = boonList.GetBoon(boonItem.id);
        if (isUpgrade)
        {
            effectAmount = boon.EffectAmountToUpgrade;
        }
        else
        {
            effectAmount = boon.EffectAmountCurrent;
        }

        if(boonItem.isPercentage)
            boonTransform.Find("descText").GetComponent<TextMeshProUGUI>().SetText($"{boonItem.description} {effectAmount * 100}%");
        else
            boonTransform.Find("descText").GetComponent<TextMeshProUGUI>().SetText($"{boonItem.description} {effectAmount}");
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
