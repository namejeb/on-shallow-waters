using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] dotTexts;
    [SerializeField] private float delay = .5f;

    private bool _isResetting = false;
    
    private float _nextEnableTime = 0f;
    private int _nextDotCounter = 0;

    private void Start()
    {
        DisableAllDots();
    }
    
    private void Update()
    {
        if (Time.time > _nextEnableTime && !_isResetting)
        {
            dotTexts[_nextDotCounter].enabled = true;
            UpdateCounter();
            _nextEnableTime = Time.time + delay;
        }
    }

    private void UpdateCounter()
    {
        _nextDotCounter++;
        if (_nextDotCounter == dotTexts.Length)
        {
            _nextDotCounter = 0;
            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        if (_isResetting) yield return null;
        _isResetting = true;
        
        yield return new WaitForSeconds(delay);
        DisableAllDots();
        _isResetting = false;
        _nextEnableTime = Time.time + delay;
    }

    private void DisableAllDots()
    {
        for (int i = 0; i < dotTexts.Length; i++)
        {
            dotTexts[i].enabled = false;
        }
    }
}
