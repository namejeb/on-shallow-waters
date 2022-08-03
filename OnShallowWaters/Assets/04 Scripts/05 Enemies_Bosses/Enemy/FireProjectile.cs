using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private GameObject crabProjectile;
    private Quaternion _rotate;
    private Vector3 _direction;
    public Transform firePoint;

    private Transform target;
    public FastShooter fastShooter1;

    private void Awake(){
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable(){
        Attack();
    }

    private void Attack(){
        Transform trans;
        (trans = transform).LookAt(target);
        _direction = target.position - trans.position;
        _rotate = Quaternion.LookRotation(_direction);

        Quaternion rotation = _rotate;
        Instantiate(crabProjectile, firePoint.position, rotation);
        fastShooter1.bulletFired += 1;
    }
}
