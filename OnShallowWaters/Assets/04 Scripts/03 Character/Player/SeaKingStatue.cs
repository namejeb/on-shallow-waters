using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SeaKingStatue : MonoBehaviour, IPointerDownHandler
{
    /*
     
        NOTES: IF IMPLEMENT SKB STATUE IN SCENE, PLEASE ADD [PHYSICS RAYCASTER] COMPONENT TO THE CAMERA

     */


    public enum Blessing { bless1, bless2 , bless3 , bless4 , bless5 }

    public string blessName, blessDesc;
    public float soulDuration, requiredSoul;
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

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        SKB_UIDataHolder dataHolder = GameObject.FindGameObjectWithTag("SKB_UIDataHolder").GetComponent<SKB_UIDataHolder>();

        BlessUI = dataHolder.BlessUI;
        bg = dataHolder.bg;
        blessNameText = dataHolder.blessNameText;
        descriptionText = dataHolder.descriptionText;
        equipButton = dataHolder.equipButton;
        exitButton = dataHolder.exitButton;
        skbButton = dataHolder.skbButton;
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
            print('s');
            BlessUI.SetActive(true);
            bg.enabled = true;
            equipButton.onClick.AddListener(ChangeBlessing);
            blessNameText.text = blessName;
            descriptionText.text = "Time: " + soulDuration.ToString() + "\tRequired Souls: " + requiredSoul.ToString() + "\n" + blessDesc;
        }
    }

    public void ChangeBlessing()
    {
        skbButton.onClick.RemoveAllListeners();
        skb.Duration = soulDuration;
        skb.RequiredSoul = requiredSoul;
        switch (blessType)
        {
            case Blessing.bless1:
                skbButton.onClick.AddListener(skb.SKB1);
                break;
            case Blessing.bless2:
                skbButton.onClick.AddListener(skb.SKB2);
                skb.Duration = skb.Skb2Duration;
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
