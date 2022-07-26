using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TutorialManager : MonoBehaviour, IPointerDownHandler
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
        if (_descCounter == descriptionTexts.Length - 1){
            // enable task part
            if(SwitchStage != null) SwitchStage();
        } else {
            SetObjectActive(descriptionTexts[_descCounter], true);
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
