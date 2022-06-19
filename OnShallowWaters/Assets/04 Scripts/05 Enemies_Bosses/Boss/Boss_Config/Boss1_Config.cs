using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Config : MonoBehaviour
{
    public float dashTimeout = 3f;
    public float dashSpeed;

    private float _percentage = 0.7f;

    private Boss_Stats _bs;

    private void Awake()
    {
        _bs = GetComponent<Boss_Stats>();
    }

    private void Update()
    {
        if (_bs.CurrHpPercentage <= _percentage && !_bs.armState)
        {
            _percentage -= 0.3f;
            _bs.armState = true;
            Debug.Log(_bs.CurrHpPercentage + " Dectected 1");
        }
    }
}
