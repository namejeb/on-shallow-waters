using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class TreasureChest : EarnCurrencyItems, IDamageable
{
    [SerializeField] [Range(0f, 1f)] private float spawnRate = .2f;
    private Animator _anim;

    private bool _isOpened = false;
    
    private void Awake()
    {
        float chance = Random.Range(0f, 1f);

        if (chance > spawnRate)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;

        OpenChest();
    }

    private void OpenChest()
    {
        _anim.enabled = true;
        EarnGold(minMaxAmount.x, minMaxAmount.y);
    }
    
    public void Damage(int damageAmount)
    {
        OpenChest();
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
    
    
    // private IEnumerator FadeOut()
    // {
    //     MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
    //     while (meshRenderer.material.color.a > 0)
    //     {
    //         print('s');
    //         Color color = meshRenderer.material.color;
    //         color.a -= Time.deltaTime * 1f;
    //         meshRenderer.material.color = color ;
    //         yield return null;
    //     }
    // }


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
