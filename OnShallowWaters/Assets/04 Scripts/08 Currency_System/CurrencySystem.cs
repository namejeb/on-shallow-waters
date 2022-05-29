using UnityEngine;
using System.Collections.Generic;
using System;

public enum CurrencyType
{
    SOULS,
    GOLD,
}

public class CurrencySystem : MonoBehaviour
{
    public static Dictionary<CurrencyType, int> currencyDict = new Dictionary<CurrencyType,int>();

    public static event Action OnCurrencyChanged;

    private void OnDestroy()
    {
       // OnCurrencyChanged -= Print;
    }
    
    private void Awake()
    {
        currencyDict.Clear();
        
        //initialise dictionary
        for (int i = 0; i < 2; i++)
        {
            currencyDict.Add((CurrencyType) i, 0);
        }

        // AddCurrency(CurrencyType.GOLD, 50);
        // AddCurrency(CurrencyType.SOULS, 80);
        // Print();
        // OnCurrencyChanged += Print;
    }
    
    public static void AddCurrency(CurrencyType currencyType, int amount)
    {
        currencyDict[currencyType] += amount;
        
        if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke();
    }

    //Main function for shop systems
    public static void RemoveCurrency(CurrencyType currencyType, int amount)
    {
        int currAmt = currencyDict[currencyType];
        if (currAmt - amount > 0)
        {
            currencyDict[currencyType] -= amount;
        }
        else
        {
            currencyDict[currencyType] = 0;
        }
        if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke();
    }

    private void Print()
    {
        print("Current GOLD: " + currencyDict[CurrencyType.GOLD]);
        print("Current SOULS: " + currencyDict[CurrencyType.SOULS]);
    }
}
