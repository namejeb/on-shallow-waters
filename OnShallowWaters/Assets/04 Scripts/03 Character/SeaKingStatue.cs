using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaKingStatue : MonoBehaviour
{
    public enum Blessing { bless1, bless2 , bless3 , bless4 , bless5 }

    [SerializeField] private Button skbButton;

    public Blessing blessType;

    private void OnTriggerEnter(Collider col)
    {
        SkBlessing skb = col.gameObject.GetComponent<SkBlessing>();
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
    }
}
