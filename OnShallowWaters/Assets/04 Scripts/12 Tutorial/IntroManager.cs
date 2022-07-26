using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour, IPointerDownHandler
{
    [Header("Descriptions of each controls")]
    [SerializeField] private Transform[] descriptionTexts;
    private int _descCounter = 0;

    public static event Action SwitchStage;

    public void OnPointerDown(PointerEventData eventData)
    {
        // tapped
        _descCounter++;
        
        // If finished all descriptionTexts
        if (_descCounter == descriptionTexts.Length){
      
            // disable last text
            SetObjectActive(descriptionTexts[_descCounter- 1], false);
            
            // disable image to disallow more tapping
            GetComponent<Image>().enabled = false;
            
            // enable task part
            if(SwitchStage != null) SwitchStage();
            
        } else {
            SetObjectActive(descriptionTexts[_descCounter], true);
            
            // disable previous text
            if (_descCounter - 1 >= 0)
            {
                SetObjectActive(descriptionTexts[_descCounter- 1], false);
            }
        }
    }

    private void Start()
    {
        Init();
        SetObjectActive(descriptionTexts[0], true);
    }

    private void Init()
    {
        for (int i = 0; i < descriptionTexts.Length; i++)
        {
            SetObjectActive(descriptionTexts[i], false);
        }
    }

    private void SetObjectActive(Transform uiTransform, bool status)
    {
        uiTransform.gameObject.SetActive(status);
    }
}
