using System.Collections;
using UnityEngine;


public class EnemiesProjectile : MonoBehaviour{
    public float speed = 10;
    public float fireRate;
    public int bulletDamage = 10;
    public Vector3 dir;

    private void OnEnable(){
        StartCoroutine(Removed());
    }
        
    protected virtual void Update() {
        Transform trans = transform;
        trans.position += dir.normalized * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            //print("Hit");
            other.GetComponent<PlayerStats>().Damage(bulletDamage);
        }

        gameObject.SetActive(false);
    }

    private IEnumerator Removed(){
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}

