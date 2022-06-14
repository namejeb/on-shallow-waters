using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public class LightAttacker : EnemiesCore {
        private Vector3 _dir;
        [SerializeField] private float force = 10;
        
        protected override void Attack(){
            _dir = puppet.position - transform.position;
            _dir = _dir.normalized;
            rb3d.AddForce(_dir * force);
        }
    }
}
