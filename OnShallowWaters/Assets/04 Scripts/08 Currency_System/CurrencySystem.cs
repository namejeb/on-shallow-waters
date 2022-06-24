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

    public static event Action<Vector2Int> OnCurrencyChanged;
    private static Vector2Int _goldSoulAmount;
    
    // private void OnDestroy()
    // {
    //     OnCurrencyChanged -= Print;
    // }
    
    private void Awake()
    {
        currencyDict.Clear();
        
        //initialise dictionary
        for (int i = 0; i < 2; i++)
        {
            currencyDict.Add((CurrencyType) i, 0);
        }

        AddCurrency(CurrencyType.GOLD, 50);
        AddCurrency(CurrencyType.SOULS, 80);
         // Print(_goldSoulAmount);
         // OnCurrencyChanged += Print;
    }
    
    public static void AddCurrency(CurrencyType currencyType, int amount)
    {
        currencyDict[currencyType] += amount;
        UpdateVector2Int();
        
        if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke(_goldSoulAmount);
    }

    //Overload for adding in a range
    public static void AddCurrency(CurrencyType currencyType, int minAmount, int maxAmount)
    {
        int amountToAdd = UnityEngine.Random.Range(minAmount, maxAmount);
        AddCurrency(currencyType, amountToAdd);
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
        UpdateVector2Int();
        
        if(OnCurrencyChanged != null) OnCurrencyChanged.Invoke(_goldSoulAmount);   
    }

    private static void UpdateVector2Int()
    {
        _goldSoulAmount.x = currencyDict[CurrencyType.GOLD];
        _goldSoulAmount.y = currencyDict[CurrencyType.SOULS];
    }
    

    // private void Print(Vector2Int amounts)
    // {
    //     print("Current GOLD: " + currencyDict[CurrencyType.GOLD]);
    //     print("Current SOULS: " + currencyDict[CurrencyType.SOULS]);
    // }
}
