using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public class Buffer : EnemiesCore{
        [SerializeField] private float r;
        private bool _once;

        protected override void Attack(){
            AreaBuff();
        }

        private void AreaBuff(){
            if (_once) return;
            _once = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, r);
            foreach (var hitCollider in colliders){
                if (hitCollider.CompareTag("Enemy")){
                    hitCollider.GetComponent<EnemiesCore>().ReceivedBuff();
                }
            }
        }

        // void OnCollisionEnter(Collision c){
        //     foreach (ContactPoint contact in c.contacts){
        //         print("c: " + contact.point);
        //     }
        // }
    }
}