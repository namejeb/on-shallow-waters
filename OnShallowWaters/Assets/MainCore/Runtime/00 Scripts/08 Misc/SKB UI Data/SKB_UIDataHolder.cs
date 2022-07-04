using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SKB_UIDataHolder : MonoBehaviour
{
    [Header("UI Side")]
    public GameObject BlessUI;
    public Image bg;
    public TMP_Text blessNameText, descriptionText;
    public Button equipButton;
    public Button exitButton;

    [Header("Player Side")]
    public Button skbButton;
}
