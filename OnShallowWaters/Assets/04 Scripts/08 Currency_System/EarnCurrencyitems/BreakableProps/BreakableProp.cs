using UnityEngine;

public class BreakableProp : EarnCurrencyItems, IDamageable
{
    [SerializeField] private Transform brokenVerPrefab;

    private MeshRenderer _meshRenderer;
    private Collider _collider;
    public SoundData BreakableSFX;
    private VFXCurrency _vfxCurrency;

    public const string BREAKABLE_KEY = "BreakableCoins";
    private void Awake()
    {
        _meshRenderer =GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _vfxCurrency = GetComponent<VFXCurrency>();
    }

    private void OnEnable()
    {
        SetPropActive(true);
    }

    public void Damage(int damageAmount)
    {
        Break();
    }

    public float GetReceivedDamage(float outDamage)
    {
        return outDamage;
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
    
    private void Break()
    {
        //play sound
      
        //function
        EarnGold(minMaxAmount.x, minMaxAmount.y);
        
        //visual
  
        Vector3 rot = transform.eulerAngles;
        Vector3 rotOffset = new Vector3(90f, 0f, 0f);
        Quaternion targetRot = Quaternion.Euler(rot + rotOffset);

        Instantiate(brokenVerPrefab, transform.position, targetRot);
        SetPropActive(false);
        SoundManager.instance.PlaySFX(BreakableSFX,"Barrel SFX");
    }

    private void SetPropActive(bool status)
    {
        _meshRenderer.enabled = status;
        _collider.enabled = status;
    }
}
