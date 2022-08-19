using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PlayerHealthBar : MonoBehaviour
{
    [Header("BackSlider colors: ")]
    [SerializeField] private Color healColor = Color.yellow;
    [SerializeField] private Color damagedColor = Color.red;
    
    [Space][Space]
    [Header("Sliders: ")]
    [SerializeField] private Slider backSlider;
    [SerializeField] private Slider frontSlider;
    
    [Space][Space]
    [Header("Images: ")]
    [SerializeField] private Image backImage;
    [SerializeField] private Image frontImage;

    private Transform _hpBarContainer;
    

    private void OnDestroy()
    {
        LeanTween.reset();
    }

    private void Start()
    {
        _hpBarContainer = backSlider.transform.parent;
    }

    public void SetMaxHealth(float health)
    {
        frontSlider.maxValue = health;
        frontSlider.value = health;
        
        backSlider.maxValue = health;
        backSlider.value = health;
    }
    public void SetHealth(float health, bool isHeal)
    {
        float fastSpeed = .01f;
        float slowSpeed = .8f;
        
        if (isHeal)
        {
            LeanTween.value(frontSlider.value, health, slowSpeed).setOnUpdate( val => frontSlider.value = val);
            LeanTween.value(backSlider.value, health, fastSpeed).setOnUpdate( val => backSlider.value = val);
            backImage.color = healColor;
        }
        else
        {
            LeanTween.value(frontSlider.value, health, fastSpeed).setOnUpdate( val => frontSlider.value = val);
            LeanTween.value(backSlider.value, health, slowSpeed).setOnUpdate( val => backSlider.value = val);
            backImage.color = damagedColor;
            TryShake();
        }
    }

    private void TryShake()
    {
        float rate = 1;
        float rng = Random.Range(0f, 1f);
        if (rng < rate)
        {
            StartCoroutine(Shake(.5f));
        }
    }

    private IEnumerator Shake(float duration)
    {
        Vector3 startPos = _hpBarContainer.position;
        float startTime = duration;
        float magnitude = 3.5f;
        
        while (startTime > 0f)
        {
            LeanTween.move(_hpBarContainer.gameObject, startPos + (GetDirection() * magnitude), .03f);
            startTime -= Time.deltaTime;
            yield return null;
        }
        
        // return to original position
        LeanTween.move(_hpBarContainer.gameObject, startPos, .03f);
    }

    private Vector3 GetDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
