using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private DashNAttack dna;
    private LayerMask _damageableLayer;

    private void Start()
    {
        _damageableLayer = dna.DamageableLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ~_damageableLayer) return;
        dna.HandleDamaging(other);
    }
}
