using UnityEngine;
using System.Collections.Generic;
using System;

public enum CurrencyType
{
    SOULS,
    GOLD
}

public class CurrencySystem : MonoBehaviour
{
    public static Dictionary<CurrencyType, int> currencyDict = new Dictionary<CurrencyType,int>();

    //public static event Action OnCurrencyChanged;
    private void Awake()
    {
        currencyDict.Clear();
        
        //initialise dictionary
        for (int i = 0; i < 2; i++)
        {
            currencyDict.Add((CurrencyType) i, 0);
        }
    }

    public void AddGold(int amount)
    {
        currencyDict[CurrencyType.GOLD] += amount;
        
      //  if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke();
    }
    
    public void RemoveGold(int amount)
    {
        int currAmt = currencyDict[CurrencyType.GOLD];
        if (currAmt - amount > 0)
        {
            currencyDict[CurrencyType.GOLD] -= amount;
        }
        else
        {
            currencyDict[CurrencyType.GOLD] = 0;
        }
        
      //  if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke();
    }
    
    public void AddSouls(int amount)
    {
        currencyDict[CurrencyType.SOULS] += amount;
        
     //   if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke();
    }
    
    public void RemoveSouls(int amount)
    {
        int currAmt = currencyDict[CurrencyType.SOULS];
        if (currAmt - amount > 0)
        {
            currencyDict[CurrencyType.SOULS] -= amount;
        }
        else
        {
            currencyDict[CurrencyType.SOULS] = 0;
        }
        
    //    if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke();
    }
}
