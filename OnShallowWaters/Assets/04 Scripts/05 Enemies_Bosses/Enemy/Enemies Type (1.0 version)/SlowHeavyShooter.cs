using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public class SlowHeavyShooter : EnemiesCore{
        public Transform firePoint;
        
        [SerializeField] private GameObject effectToSpawn;
        [SerializeField] private float timeToFire;
        private EnemiesProjectile _enemiesProjectile;
        private Vector3 _direction;
        private Quaternion _rotate;
        private Vector3 _pos;

        private int firedNum;
        public int firedLimit;

        private void Awake(){
            shieldRecover = true;
            currentShield = maxShield;
        }

        protected override void Start(){
            base.Start();
            _enemiesProjectile = effectToSpawn.GetComponent<EnemiesProjectile>();
        }

        protected override void Movement(){
            behaviour = CoreStage.Attack;
        }

        protected override void Attack(){
            if (dist > radius * radius){
                behaviour = CoreStage.Idle;
                return;
            }

            FireBullet();
        }

        protected virtual void FireBullet(){
            Transform trans;
            (trans = transform).LookAt(puppet);
            _direction = puppet.position - trans.position;
            _rotate = Quaternion.LookRotation(_direction);

            if (Time.time >= timeToFire){
                timeToFire = Time.time + (1 / _enemiesProjectile.fireRate);

                Quaternion rotation = _rotate;
                Instantiate(effectToSpawn, firePoint.position, rotation);

                firedNum += 1;
            }

            if (firedNum != firedLimit) return;
            behaviour = CoreStage.Move;
            firedNum = 0;
        }
    }
}
