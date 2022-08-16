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
    public static Dictionary<CurrencyType, int> currencyDict = new Dictionary<CurrencyType, int>();

    public static event Action<Vector2Int> OnCurrencyChanged;
    
    private static Vector2Int _goldSoulAmount;
    public static Vector2Int GoldSoulAmount { get => _goldSoulAmount; }
    
    private void Awake()
    {
        currencyDict.Clear();
        
     
    }

    private void Start()
    {
        _goldSoulAmount.x =  PlayerPrefs.GetInt(TreasureChest.TREASURE_KEY);
    
        currencyDict.Add(CurrencyType.GOLD, _goldSoulAmount.x);
        currencyDict.Add(CurrencyType.SOULS, 0);

        if (OnCurrencyChanged != null)  OnCurrencyChanged.Invoke(_goldSoulAmount); 
    }

    public static void AddCurrency(CurrencyType currencyType, int amount)
    {
        currencyDict[currencyType] += amount;
        UpdateVector2Int();
        
        if (OnCurrencyChanged != null)  OnCurrencyChanged.Invoke(_goldSoulAmount); 
    }

    //Overload for adding in a range
    public static void AddCurrency(CurrencyType currencyType, int minAmount, int maxAmount)
    {
        int amountToAdd = UnityEngine.Random.Range(minAmount, maxAmount);
        UpdateVector2Int();
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

        int gold = currencyDict [CurrencyType.GOLD];

        PlayerPrefs.SetInt(TreasureChest.TREASURE_KEY, gold);
    }
}
