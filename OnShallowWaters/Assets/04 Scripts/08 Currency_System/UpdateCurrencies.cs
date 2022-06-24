using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCurrencies : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldAmountText;
    [SerializeField] private TextMeshProUGUI soulAmountText;

    private void OnDestroy()
    {
        CurrencySystem.OnCurrencyChanged -= UpdateUI;
    }
    private void Awake()
    {
        CurrencySystem.OnCurrencyChanged += UpdateUI;
    }

    private void UpdateUI(Vector2Int goldSoulAmount)
    {
        goldAmountText.text = goldSoulAmount.x.ToString();
        soulAmountText.text = goldSoulAmount.y.ToString();
    }
}
