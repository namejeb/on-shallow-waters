using System.Collections;
using UnityEngine;


    public class EnemiesProjectile : MonoBehaviour{
        public float speed = 10;
        public float fireRate;
        public int bulletDamage = 10;

        private void Start(){
            StartCoroutine(Removed());
        }
        
        protected virtual void Update() {
            Transform trans = transform;
            trans.position += trans.forward * (speed * Time.deltaTime);
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

