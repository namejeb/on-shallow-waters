using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public class EnemiesProjectile : MonoBehaviour{
        public float speed = 10;
        public float fireRate;

        private void Start(){
            StartCoroutine(Removed());
        }
        
        protected virtual void Update() {
            Transform trans = transform;
            trans.position += trans.forward * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other){
            if (!other.CompareTag("Enemy")){
                AreaImpact();
                gameObject.SetActive(false);
            }
            
            //! IF PLAYER, APPLY DMG
        }
        
        protected virtual void AreaImpact(){
            //Does nothing for now
        }

        private IEnumerator Removed(){
            yield return new WaitForSeconds(4.5f);
            gameObject.SetActive(false);
        }
    }
}
