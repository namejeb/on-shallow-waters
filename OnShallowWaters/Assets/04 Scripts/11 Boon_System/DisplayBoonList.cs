using UnityEngine;
using System.Collections.Generic;


public class DisplayBoonList : MonoBehaviour
{
    [SerializeField] private float offsetX;

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
                                                                                                                                                                                  
        float shopButtonWidth = newDisplayRect.rect.width;                                                                                                            
        newDisplayRect.anchoredPosition = new Vector2(shopButtonWidth * _numOfDisplays * -offsetX, newDisplayRect.anchoredPosition.y);

        _numOfDisplays++;
        
        _createdDisplays.Add(newDisplay);
    }

    private void SetInfo(List<BoonItemsTimesUsed> boonItemsTimesUsedList)
    {
        //set texts
        //set counter

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
        if (isNewBoon)
        {
            CreateDisplay();
        }
        
        SetInfo(boonItemsTimesUsedList);
    }

    private void UpdateLevelIndicators(Transform displayTransform, BoonItemsTimesUsed boonItemsTimesUsed)
    {
        Transform upgradeLevels = displayTransform.Find("upgradeLevels");
        Transform[] indicators = new Transform[3];

        for (int i = 0; i < upgradeLevels.childCount; i++)
        {
            indicators[i] = upgradeLevels.transform.GetChild(i);
            indicators[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < boonItemsTimesUsed.usageCount; i++)
        {
            indicators[i].gameObject.SetActive(true);
        }
    }
}
