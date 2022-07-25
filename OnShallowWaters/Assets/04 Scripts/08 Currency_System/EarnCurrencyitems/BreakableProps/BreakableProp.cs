using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class BreakableProp : EarnCurrencyItems, IDamageable
{
    [SerializeField] private Transform brokenVerPrefab;

    private MeshRenderer _meshRenderer;
    private Collider _collider;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        SetPropActive(true);
    }

    public void Damage(int damageAmount)
    {
        Break();
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
    }

    private void SetPropActive(bool status)
    {
        _meshRenderer.enabled = status;
        _collider.enabled = status;
    }
}