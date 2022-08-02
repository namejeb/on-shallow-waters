using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Config : MonoBehaviour
{
    private Boss_Stats _bs;

    private float[] _percentages = {.7f, .4f, .1f};
    private int _index = 0;

    private void Awake()
    {
        _bs = GetComponent<Boss_Stats>();
    }

    private void Update()
    {
        if (_bs.CurrHpPercentage <= _percentages[_index] && !_bs.armState)
        {
            if (_index < 2)
            {
                _index++;
                _bs.armState = true;
            }
        }
    }
}
