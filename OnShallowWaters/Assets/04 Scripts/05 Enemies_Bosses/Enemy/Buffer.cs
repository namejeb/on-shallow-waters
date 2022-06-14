using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public class Buffer : EnemiesCore{
        [SerializeField] private float radius;
        private bool _once;

        protected override void Attack(){
            AreaBuff();
        }

        private void AreaBuff(){
            if (_once) return;
            _once = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in colliders){
                if (hitCollider.CompareTag("Enemy")){
                    hitCollider.GetComponent<EnemiesCore>().ReceivedBuff();
                }
            }
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        // void OnCollisionEnter(Collision c){
        //     foreach (ContactPoint contact in c.contacts){
        //         print("c: " + contact.point);
        //     }
        // }
    }
}