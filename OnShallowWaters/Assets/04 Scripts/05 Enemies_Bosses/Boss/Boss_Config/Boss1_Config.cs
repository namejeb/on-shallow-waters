using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Config : MonoBehaviour
{
    public float dashTimeout = 3f;
    public float dashSpeed;

  // private decimal _percentage = 0.7M;
    private float _percentage = .7f;
    private Boss_Stats _bs;

    private float[] _percentages = {.7f, .4f, .1f};
    private int _index = 0;

    private void Awake()
    {
        _bs = GetComponent<Boss_Stats>();
    }

    private void Update()
    {
        //Debug.Log("Current Hp percentage: " + _bs.CurrHpPercentage + "\nTriggerPercentage: " + _percentage);
        // if ((decimal)_bs.CurrHpPercentage <= _percentage && !_bs.armState)
        // {
        //     _percentage -= 0.3M;
        //     _bs.armState = true;
        // }

        if (_bs.CurrHpPercentage <= _percentages[_index] && !_bs.armState)
        {
            if (_index < 2)
            {
                _index++;
                _bs.armState = true;
            }
        }
        
        // if (_bs.CurrHpPercentage <= _percentage && !_bs.armState)
        // {
        //     _percentage -= 0.3f;
        //     _bs.armState = true;
        // }
    }
}
