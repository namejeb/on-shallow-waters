using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private GameObject crabProjectile;
    private Quaternion _rotate;
    private Vector3 _direction;
    public Transform firePoint;

    private Transform target;
    private EnemyPooler _enemyPooler;
    public FastShooter fastShooter1;

    private void Awake(){
        target = GameObject.FindWithTag("Player").transform;
        _enemyPooler = FindObjectOfType<EnemyPooler>();
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
        //Instantiate(crabProjectile, firePoint.position, rotation);
        Transform bullet = _enemyPooler.GetFromPool(ProjectileType.Enemy3Ball);
        bullet.position = firePoint.position;
        //bullet.rotation = rotation;
        bullet.gameObject.SetActive(true);
        fastShooter1.bulletFired += 1;
    }
}
