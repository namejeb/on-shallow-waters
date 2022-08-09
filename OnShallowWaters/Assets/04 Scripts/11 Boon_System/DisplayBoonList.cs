using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class DisplayBoonList : MonoBehaviour
{
    [Header("Settings:")]
    [SerializeField] private float offsetWidth;
    [SerializeField] private Vector2 offsetsPos;

    [Space][Space]
    [Header("Ref:")]
    [SerializeField] private Button backButton;
    
    [Space][Space]
    [SerializeField] private Transform chosenBoonList;
    private bool _isOpened = false;

    [Space][Space]
    [Header("Info Display: ")]
    [SerializeField] private Transform infoDisplay;
    [SerializeField] private Transform exitInfoDisplayButton;
    [SerializeField] private Transform bgButton;
    
    
    private Transform _containerDisplayList;      
    private Transform _boonDisplayTemplate;
    private Transform _scrollContent;
    
    private List<Transform> _createdDisplays = new List<Transform>();

    private BoonSelection _boonSelection;

    private int _row;
    private int _col;

    public void ExitDisplayInfo()
    {
        infoDisplay.gameObject.SetActive(false);
        exitInfoDisplayButton.gameObject.SetActive(false);
    }
    
    private void ExitChosenBoons()
    {
        ExitDisplayInfo();
        ToggleList();
    }
    private void OnDestroy()
    {
        BoonSelection.OnListChanged -= UpdateList;
        backButton.onClick.RemoveListener(ExitChosenBoons);
    }

    private void Start()
    {
        BoonSelection.OnListChanged += UpdateList;

        _boonSelection = GetComponent<BoonSelection>();
        
        SetupDisplayList();
        chosenBoonList.gameObject.SetActive(false);
        
        infoDisplay.gameObject.SetActive(false);
        exitInfoDisplayButton.gameObject.SetActive(false);
        
        Button_UI buttonUI = bgButton.GetComponent<Button_UI>();
        buttonUI.ClearAllListeners();
        buttonUI.ClickEvent(() => ExitChosenBoons() );
        
        backButton.onClick.AddListener(ExitChosenBoons);
    }
    
    private void SetupDisplayList()                                      
    {                                                                    
        _containerDisplayList = transform.Find("container_chosenBoons"); 
        _scrollContent = _containerDisplayList.Find("ScrollView").Find("Content");
        _boonDisplayTemplate = _scrollContent.Find("boonDisplayTemplate");
        
        _boonDisplayTemplate.gameObject.SetActive(false);                
    }

    private void CreateDisplay()
    {
        Transform newDisplay = Instantiate(_boonDisplayTemplate, _scrollContent);                                                                                   
        RectTransform newDisplayRect = newDisplay.GetComponent<RectTransform>();         
        newDisplay.gameObject.SetActive(true);

        float shopButtonHeight = newDisplayRect.rect.height + 10f;
        float shopButtonWidth = newDisplayRect.rect.width;                                                                                                            
        newDisplayRect.anchoredPosition = new Vector2(offsetsPos.x + shopButtonWidth * offsetWidth * _col, offsetsPos.y + shopButtonHeight * -_row);

        _createdDisplays.Add(newDisplay);

        // Coordinates
        _col++;
        if (_col == 3)
        {
            _col = 0;
            _row++;
            RelocateContents(shopButtonHeight);
        }
    }

    private void RelocateContents(float shopButtonHeight)
    {
        RectTransform contentRect = _scrollContent.GetComponent<RectTransform>();
        contentRect.sizeDelta += new Vector2(0f, shopButtonHeight + 60f);
            
        //offset by half the height increased
        float heightOffset = shopButtonHeight * .5f;
        contentRect.anchoredPosition += new Vector2(0f, -heightOffset);   //move down
        RelocateBoonDisplays(heightOffset);   // move up
    }
    private void RelocateBoonDisplays(float height)
    {
        int size = _scrollContent.childCount;
        for (int i = 0; i < size; i++)
        {
            Transform boonDisplay = _scrollContent.GetChild(i);
            boonDisplay.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f, height);
        }
    }

    private void DisplayInfo(BoonItemsTimesUsed boonItemsTimesUsed, Transform buttonTransform)
    {
        exitInfoDisplayButton.gameObject.SetActive(true);
        infoDisplay.gameObject.SetActive(true);

        // Info setting
        Transform infoDisplayTemplate = infoDisplay.Find("infoDisplayTemplate");
        _boonSelection.SetBoonInfo(infoDisplayTemplate, boonItemsTimesUsed.boonItem, false);
        UpdateLevelIndicators(infoDisplayTemplate, boonItemsTimesUsed);

        TextMeshProUGUI text = infoDisplayTemplate.Find("typeText").GetComponent<TextMeshProUGUI>();
        SetBoonType(text, boonItemsTimesUsed.boonItem.boonType);
        
        
        // Animation
        infoDisplay.localScale = new Vector3(0f, 0f, 0f);
        RectTransform infoDisplayRect = infoDisplay.GetComponent<RectTransform>();
        infoDisplayRect.position = buttonTransform.position;
     
        LeanTween.scale(infoDisplayRect, Vector3.one, .15f);
        LeanTween.move(infoDisplayRect, Vector3.zero, .15f);
    }

    private void SetBoonType(TextMeshProUGUI text, BoonType boonType)
    {
        string str = "";
        switch (boonType)
        {
            case BoonType.COMBAT:
                str = "-COMBAT-";
                break;
            case BoonType.SURVIVAL:
                str = "-SURVIVAL-";
                break;
            case BoonType.BONUS:
                str = "-BONUS-";
                break;
        }

        text.text = str;
    }

    private void SetInfo(List<BoonItemsTimesUsed> boonItemsTimesUsedList)
    {
        // list needs to be enabled first to add button function
        ToggleList();
        
        List<Transform> reversedDisplayList = _createdDisplays;
        reversedDisplayList.Reverse();

        List<BoonItemsTimesUsed> reversedBoonItemsList = boonItemsTimesUsedList;
        reversedBoonItemsList.Reverse();

        for (int i = 0; i < reversedDisplayList.Count; i++)
        {
            Transform currDisplay = reversedDisplayList[i];
            BoonItemsTimesUsed currBoonItemsTimesUsed = reversedBoonItemsList[i];
            
            _boonSelection.SetBoonInfo(currDisplay, currBoonItemsTimesUsed.boonItem);
            UpdateLevelIndicators(currDisplay, currBoonItemsTimesUsed);
            
            // info display
            Button_UI buttonUI = currDisplay.Find("icon").GetComponent<Button_UI>();
            
            //Remove previous clickEvents
            buttonUI.ClearAllListeners();          
            
            buttonUI.ClickEvent(() => DisplayInfo(currBoonItemsTimesUsed, buttonUI.transform ) );
        }
        
        // close list again
        ToggleList();
    }

    private void UpdateList(List<BoonItemsTimesUsed> boonItemsTimesUsedList, bool isNewBoon)
    {
        if (isNewBoon) { CreateDisplay(); }
        SetInfo(boonItemsTimesUsedList);
    }

    private void UpdateLevelIndicators(Transform displayTransform, BoonItemsTimesUsed boonItemsTimesUsed)
    {
        Transform indicators = displayTransform.Find("upgradeLevels").Find("Indicators");
        
        Transform unfilledIndicatorsContainer = indicators.Find("unfilled");
        Transform filledIndicatorsContainer = indicators.Find("filled");

        Transform[] unfilledIndicators = new Transform[4];
        Transform[] filledIndicators = new Transform[4];

        // disable all first
        for (int i = 0; i < filledIndicatorsContainer.childCount; i++)
        {
            // unfilled
            unfilledIndicators[i] = unfilledIndicatorsContainer.GetChild(i);
            SetIndicatorActive(unfilledIndicators[i], false);
            
            // filled
            filledIndicators[i] = filledIndicatorsContainer.GetChild(i);
            SetIndicatorActive(filledIndicators[i], false);
        }

        int maxUsageCount = boonItemsTimesUsed.boonItem.maxUsageCount;

        // enable back according to maxUsageCount
        for (int i = 0; i < maxUsageCount; i++)
        {
            SetIndicatorActive(unfilledIndicators[i], true);
        }


        // enable back according to usageCount
        for (int i = 0; i < boonItemsTimesUsed.usageCount; i++)
        {
            SetIndicatorActive(filledIndicators[i], true);
        }
        PositionIndicators(indicators.parent, maxUsageCount);
    }

    public void ToggleList()
    {
        _isOpened = !_isOpened;
        chosenBoonList.gameObject.SetActive(_isOpened); 
    }

    private void SetIndicatorActive(Transform indicatorTransform, bool status)
    {
        indicatorTransform.gameObject.SetActive(status);
    }

    private void PositionIndicators(Transform indicatorsTransform, int maxUpgradeLevels)
    {
        Transform pivotContainer = indicatorsTransform.Find("Pivots");
        Transform[] pivots = new Transform[pivotContainer.childCount];

        // get children in reversed order
        for (int i = 0; i < pivots.Length; i++)
        {
            pivots[i] = pivotContainer.GetChild(i);
        }

        pivots = ReverseArray(pivots);

        int index = maxUpgradeLevels - 1;

        RectTransform indicatorsRectTransform = indicatorsTransform.GetComponent<RectTransform>();
        Vector2 currPos = indicatorsRectTransform.anchoredPosition;
        Vector2 newPos = pivots[index].GetComponent<RectTransform>().anchoredPosition;

        indicatorsRectTransform.anchoredPosition = new Vector2(newPos.x, currPos.y);
    }

    private Transform[] ReverseArray(Transform[] arrayToReverse)
    {
        Transform[] reversedArray = new Transform[arrayToReverse.Length];
        int j = arrayToReverse.Length - 1;
        for (int i = 0; i < reversedArray.Length; i++)
        {
            reversedArray[i] = arrayToReverse[j];
            j--;
        }
        
        return reversedArray;
    }
}
