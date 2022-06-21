using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public sealed class SlowHeavyShooter : EnemiesCore{
        public Transform firePoint;
        // public List<GameObject> vfx = new List<GameObject>();
        [SerializeField] private GameObject effectToSpawn;
        [SerializeField] private float timeToFire;
        private EnemiesProjectile _enemiesProjectile;
        private Vector3 _direction;
        private Quaternion _rotate;
        private Vector3 _pos;

        protected override void Start(){
            base.Start();
            //_effectToSpawn = vfx[0];
            shieldRecover = true;
            currentShield = maxShield;
            HealthBar(10);
            _enemiesProjectile = effectToSpawn.GetComponent<EnemiesProjectile>();
        }

        protected override void Movement(){
            behaviour = CoreStage.Attack;
        }

        protected override void Attack(){
            if (dist > detectRange * detectRange){
                behaviour = CoreStage.Idle;
                return;
            }

            Transform trans;
            (trans = transform).LookAt(puppet);
            _direction = puppet.position - trans.position;
            _rotate = Quaternion.LookRotation(_direction);

            if (Time.time >= timeToFire){
                timeToFire = Time.time + (1 / _enemiesProjectile.fireRate);
                Quaternion rotation = _rotate;
                Instantiate(effectToSpawn, firePoint.position, rotation);
            }
        }
    }
}
