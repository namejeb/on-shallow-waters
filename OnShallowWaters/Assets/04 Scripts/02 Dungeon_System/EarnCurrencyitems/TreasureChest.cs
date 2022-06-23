using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class TreasureChest : EarnCurrencyItems, IDamageable
{
    [SerializeField] private Vector2Int minMaxAmount;
    
    private void OpenChest()
    {
        EarnGold(minMaxAmount.x, minMaxAmount.y);
    }

    public void Damage(int damageAmount)
    {
        OpenChest();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player_WeaponCollider"))
    //     {
    //         OpenChest();
    //         gameObject.SetActive(false);
    //     }
    // }
}
