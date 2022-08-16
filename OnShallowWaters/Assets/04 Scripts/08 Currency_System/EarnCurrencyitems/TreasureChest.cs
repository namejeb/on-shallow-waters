using UnityEngine;

public class TreasureChest : EarnCurrencyItems, IDamageable
{
    [SerializeField] [Range(0f, 1f)] private float spawnRate = .2f;
    private Animator _anim;

    public const string TREASURE_KEY = "TreasureCoins";
    private void Awake()
    {
        float chance = Random.Range(0f, 1f);

        if (chance > spawnRate)
        {
            gameObject.SetActive(false);
        }
        
        //only happens to active chests
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    private void OpenChest()
    {
        _anim.enabled = true;
        EarnSoul();
    }
    
    public void Damage(int damageAmount)
    {
        OpenChest();
    }

    public float GetReceivedDamage(float outDamage)
    {
        return outDamage;
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
}
