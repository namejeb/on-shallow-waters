using UnityEngine;
using System.Collections.Generic;


public class DisplayBoonList : MonoBehaviour
{
    [SerializeField] private float offsetX;
    
    private Transform _containerDisplayList;      
    private Transform _boonDisplayTemplate;

    private int _numOfDisplays = 0;
    private List<Transform> _createdDisplays = new List<Transform>();

    private BoonSelection _boonSelection;

    private void OnDestroy()
    {
        //BoonSelection.OnListChanged -= UpdateList;
    }

    private void Start()
    {
       // BoonSelection.OnListChanged += UpdateList;

        _boonSelection = GetComponent<BoonSelection>();
        
        SetupDisplayList();
    }
    
    private void SetupDisplayList()                                      
    {                                                                    
        _containerDisplayList = transform.Find("container_chosenBoons"); 
        _boonDisplayTemplate = transform.Find("boonDisplayTemplate");    
        _boonDisplayTemplate.gameObject.SetActive(false);                
    }

    private void CreateDisplay()
    {
        Transform newDisplay = Instantiate(_boonDisplayTemplate, _containerDisplayList);                                                                                   
        RectTransform newDisplayRect = newDisplay.GetComponent<RectTransform>();                                                                          
                                                                                                                                                                                  
        float shopButtonWidth = newDisplayRect.rect.width;                                                                                                            
        newDisplayRect.anchoredPosition = new Vector2(shopButtonWidth * _numOfDisplays * offsetX, newDisplayRect.anchoredPosition.y);

        _numOfDisplays++;
        
        _createdDisplays.Add(newDisplay);
    }

    private void SetInfo(List<BoonSelection.BoonItemsTimesUsed> boonItemsTimesUsedList)
    {
        //set texts
        //set counter

        List<Transform> reversedDisplayList = _createdDisplays;
        reversedDisplayList.Reverse();

        List<BoonSelection.BoonItemsTimesUsed> reversedBoonItemsList = boonItemsTimesUsedList;
        reversedBoonItemsList.Reverse();

        for (int i = 0; i < reversedDisplayList.Count; i++)
        {
            _boonSelection.SetBoonInfo(reversedDisplayList[i], reversedBoonItemsList[i].boonItem);
        }
    }

    private void UpdateList(List<BoonSelection.BoonItemsTimesUsed> boonItemsTimesUsedList, bool isNewBoon)
    {
        if (isNewBoon)
        {
            CreateDisplay();
        }
        
        //SetInfo();
    }
}
