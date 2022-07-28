using UnityEngine;
using System.Collections.Generic;


public class DisplayBoonList : MonoBehaviour
{
    [SerializeField] private float offsetX;

    [SerializeField] private Transform chosenBoonList;
    private bool _isOpened = false;
    

    private Transform _containerDisplayList;      
    private Transform _boonDisplayTemplate;
    private Transform _scrollContent;

    private int _numOfDisplays = 0;
    private List<Transform> _createdDisplays = new List<Transform>();

    private BoonSelection _boonSelection;

    private void OnDestroy()
    {
        BoonSelection.OnListChanged -= UpdateList;
    }

    private void Start()
    {
        BoonSelection.OnListChanged += UpdateList;

        _boonSelection = GetComponent<BoonSelection>();
        
        SetupDisplayList();
        chosenBoonList.gameObject.SetActive(false);
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
                                                                                                                                                                                  
        float shopButtonWidth = newDisplayRect.rect.width;                                                                                                            
        newDisplayRect.anchoredPosition = new Vector2(shopButtonWidth * _numOfDisplays * -offsetX, newDisplayRect.anchoredPosition.y);
        
        _numOfDisplays++;
        
        _createdDisplays.Add(newDisplay);
    }

    private void SetInfo(List<BoonItemsTimesUsed> boonItemsTimesUsedList)
    {
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
        }
    }

    private void UpdateList(List<BoonItemsTimesUsed> boonItemsTimesUsedList, bool isNewBoon)
    {
        if (isNewBoon) { CreateDisplay(); }
        SetInfo(boonItemsTimesUsedList);
    }

    private void UpdateLevelIndicators(Transform displayTransform, BoonItemsTimesUsed boonItemsTimesUsed)
    {
        Transform upgradeLevels = displayTransform.Find("upgradeLevels");
        
        Transform unfilledIndicatorsContainer = upgradeLevels.Find("unfilled");
        Transform filledIndicatorsContainer = upgradeLevels.Find("filled");

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
        
        // enable back according to maxUsageCount
        for (int i = 0; i < boonItemsTimesUsed.boonItem.maxUsageCount; i++)
        {
            SetIndicatorActive(unfilledIndicators[i], true);
        }


        // enable back according to usageCount
        for (int i = 0; i < boonItemsTimesUsed.usageCount; i++)
        {
            SetIndicatorActive(filledIndicators[i], true);
        }
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
}
