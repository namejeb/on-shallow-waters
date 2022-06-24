using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class TreasureChest : EarnCurrencyItems, IDamageable
{
    [SerializeField] [Range(0f, 1f)] private float spawnRate = .2f;
    
    private bool _isOpened = false;
    
    private void Awake()
    {
        float chance = Random.Range(0f, 1f);

        if (chance > spawnRate)
        {
            gameObject.SetActive(false);
        }
    }

    private void OpenChest()
    {
        EarnGold(minMaxAmount.x, minMaxAmount.y);
    }

    public void Damage(int damageAmount)
    {
        OpenChest();
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }

    //Add animation

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player_WeaponCollider") && !_isOpened)
    //     {
    //          _isOpened = true;
    //         OpenChest();
    //         gameObject.SetActive(false);
    //     }
    // }
}
