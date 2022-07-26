using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUiManager : MonoBehaviour
{
    [SerializeField] private List<Slider> statsSlider;

    //private void Awake()
    //{
    //    for (int i = 0; i < statsSlider.Count; i++)
    //    {
    //        statsSlider[i].value = 1;
    //    }
    //}

    public void UpdateSlider(int sliderIndex, float value)
    {
        statsSlider[sliderIndex].value = value;
    }

    public bool IsActive(int sliderIndex)
    {
        return statsSlider[sliderIndex].gameObject.activeInHierarchy;
    }

    public void EnableSlider(int sliderIndex)
    {
        statsSlider[sliderIndex].gameObject.SetActive(true);
    }

    public void DisableSlider(int sliderIndex)
    {
        statsSlider[sliderIndex].gameObject.SetActive(false);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
