using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

/*  Dependencies:
 * 1. A firepoint transform where the bullet originates
*  2. A bullet trail prefab with the TrailRenderer component
*  3. A bullet impact prefab with a ParticleSystem component
 *  
*
*  Tips:
*  1. Use object pool instead of instantiating the bullet trail and bullet impact prefab for more performance
*/

public class PlayerShoot : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float distance = 20f;
    
    private float _nextFireTime = 0f;

    
    [Header("Bullet Spread")]
    [SerializeField] private bool addBulletSpread;

    [SerializeField] [Range(0f, .5f)] private float bulletSpreadVarianceX = 0f;
    [SerializeField] [Range(0f, .5f)] private float bulletSpreadVarianceY = 0f;
    [SerializeField] [Range(0f, .5f)] private float bulletSpreadVarianceZ = 0f;

   

    [Header("Effects")]
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private ParticleSystem bulletImpact;

    
    private bool _canShoot => Time.time > _nextFireTime;
    


    void Update()
    {
        if (Input.GetButton("Fire1") && _canShoot)
        {
            Shoot();
            _nextFireTime = Time.time + (1 / fireRate); 
        }
    }
    
    private void Shoot()
    {
        TrailRenderer trail = Instantiate(bulletTrail, firePoint.position, Quaternion.identity);
        
        Vector3 dir = GetBulletDirection();

        bool hitObject = Physics.Raycast(firePoint.position, dir, out RaycastHit hit, distance);
        if (hitObject)
        {
            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, dir * distance, Vector2.zero,  false));
        }
    }
    
    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint,  Vector3 hitNormal, bool madeImpact)
    {
        Vector3 startPos = trail.transform.position;
    
        float dist = Vector3.Distance(startPos, hitPoint);
        float startDist = dist;

        while (dist > 0f)
        {
            trail.transform.position = Vector3.Lerp(startPos, hitPoint, 1 - (dist / startDist));
            dist -= Time.deltaTime * bulletSpeed;

            yield return null;
        }
        
        Destroy(trail.gameObject, trail.time);
        
        //impact
        if (madeImpact)
        {
            Instantiate(bulletImpact, hitPoint, Quaternion.LookRotation(hitNormal));
        }
    }

    private Vector3 GetBulletDirection()
    {
        Vector3 dir = firePoint.forward;

        if (addBulletSpread)
        {
            Vector3 offsetDir = 
                new Vector3(Random.Range(-bulletSpreadVarianceX, bulletSpreadVarianceX),
                            Random.Range(-bulletSpreadVarianceY, bulletSpreadVarianceY),
                            Random.Range(-bulletSpreadVarianceY, bulletSpreadVarianceZ));
            dir += offsetDir;
            dir.Normalize();
        }

        return dir;
    }
}