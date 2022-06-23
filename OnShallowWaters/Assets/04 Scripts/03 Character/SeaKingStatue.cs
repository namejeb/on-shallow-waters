using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SeaKingStatue : MonoBehaviour, IPointerDownHandler
{
    public enum Blessing { bless1, bless2 , bless3 , bless4 , bless5 }

    public string blessName, blessDesc;
    public float time, requiredSoul;
    private bool isInteractable;

    [Header("UI Side")]
    [SerializeField] private GameObject BlessUI;
    [SerializeField] private Image bg;
    [SerializeField] private TMP_Text blessNameText, descriptionText;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button exitButton;

    [Header("Player Side")]
    [SerializeField] private Button skbButton;

    private SkBlessing skb;
    private Outline outline;

    public Blessing blessType;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnTriggerEnter(Collider col)
    {
        skb = col.gameObject.GetComponent<SkBlessing>();
        isInteractable = true;
        outline.OutlineWidth = 5;
    }

    private void OnTriggerExit(Collider col)
    {
        isInteractable = false;
        outline.OutlineWidth = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("STATUE CLICKED");
        if (isInteractable)
        {
            BlessUI.SetActive(true);
            bg.enabled = true;
            equipButton.onClick.AddListener(ChangeBlessing);
            blessNameText.text = blessName;
            descriptionText.text = "Time: " + time.ToString() + "\tRequired Souls: " + requiredSoul.ToString() + "\n" + blessDesc;
        }
    }

    public void ChangeBlessing()
    {
        skbButton.onClick.RemoveAllListeners();

        switch (blessType)
        {
            case Blessing.bless1:
                skbButton.onClick.AddListener(skb.SKB1);
                break;
            case Blessing.bless2:
                skbButton.onClick.AddListener(skb.SKB2);
                break;
            case Blessing.bless3:
                skbButton.onClick.AddListener(skb.SKB3);
                break;
            case Blessing.bless4:
                skbButton.onClick.AddListener(skb.SKB4);
                break;
            case Blessing.bless5:
                skbButton.onClick.AddListener(skb.SKB5);
                break;
        }

        exitButton.onClick.Invoke();
    }
}
