using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class Boon : MonoBehaviour
{
    [SerializeField] private BoonItemsSO boomItemsSo;
    [SerializeField] private float offsetX;
    
    private List<BoonItem> boonItemsList = new List<BoonItem>();
    
    private Transform _container;
    private Transform _boonButtonTemplate;

    private Transform[] _buttons = new Transform[3];
    private BoonItem[] _boonItems = new BoonItem[3];
    
    private void Start()
    {
        _container = transform.Find("container");
        _boonButtonTemplate = _container.Find("boonButtonTemplate");
        _boonButtonTemplate.gameObject.SetActive(false);
        
        for (int i = 0; i < 3; i++)
        {
            _buttons[i] = CreateShopButton(i);
        }
        
        PopulateBoonItemsPool();
        RandomiseBoonItems();
        InitButtons();
    }
    
    //Create buttons at start, only change info and onClick functions of these buttons afterwards
    private Transform CreateShopButton(int positionIndex)
    {
        Transform newShopButtonTransform = Instantiate(_boonButtonTemplate, _container);
        newShopButtonTransform.gameObject.SetActive(true);
        RectTransform newShopButtonRectTransform = newShopButtonTransform.GetComponent<RectTransform>();

        float shopButtonWidth = newShopButtonRectTransform.rect.width;
        newShopButtonRectTransform.anchoredPosition = new Vector2(shopButtonWidth * positionIndex * offsetX, newShopButtonRectTransform.anchoredPosition.y);
        
        return newShopButtonTransform;
    }
    
    //List of boons to randomize from
    private void PopulateBoonItemsPool()
    {
        //set up pool
        boonItemsList.Clear();
        foreach (BoonItem boonItem in boomItemsSo.boonItems)
        {
            boonItemsList.Add(boonItem);
        }
    }


    private void ActivateBoonEffect(int effectIndex)
    {
        switch (effectIndex)
        {
            case 0: print("fireball jutsu");
                break;
            case 1: print("kagebunshin no jutsu");
                break;
            case 2:
                print("fist");
                break;
            case 3:
                print("raiken");
                break;
        }
    }
    
    
    private void RandomiseBoonItems()
    {
        BoonItem[] boonItems = new BoonItem[3];
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, boonItemsList.Count);
            BoonItem bI = boonItemsList[index];

            boonItems[i] = bI;
            boonItemsList.Remove(bI);  //remove from pool to avoid duplicates
        }
        _boonItems = boonItems;
    }

    private void InitButtons()
    {
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
        }
    }
}
