using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private DashNAttack damage;
    private void OnTriggerEnter(Collider other)
    {
        damage.HandleDamaging(other);
    }
}
