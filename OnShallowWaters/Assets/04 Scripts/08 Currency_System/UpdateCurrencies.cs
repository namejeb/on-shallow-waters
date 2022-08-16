using System;
using TMPro;
using UnityEngine;

public class UpdateCurrencies : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldAmountText;
    [SerializeField] private TextMeshProUGUI soulAmountText;

    public static UpdateCurrencies Instance;

    private void OnDestroy()
    {
        //CurrencySystem.OnCurrencyChanged -= UpdateUI;
    }
    private void Awake()
    {
        //CurrencySystem.OnCurrencyChanged += UpdateUI;

        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI(Vector2Int goldSoulAmount)
    {
        goldAmountText.text = goldSoulAmount.x.ToString();
        soulAmountText.text = goldSoulAmount.y.ToString();
    }

    public void UpdateUI()
    {
        Vector2Int goldSoulAmount = CurrencySystem.GoldSoulAmount;
        
        goldAmountText.text = goldSoulAmount.x.ToString();
        soulAmountText.text = goldSoulAmount.y.ToString();
    }
}
