using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public sealed class SlowHeavyShooter : EnemiesCore{
        public GameObject firePoint;
        // public List<GameObject> vfx = new List<GameObject>();
        [SerializeField] private GameObject effectToSpawn;
        [SerializeField] private float timeToFire;
        private EnemiesProjectile _enemiesProjectile;
        private Vector3 _direction;
        private Quaternion _rotate;
        private Vector3 _pos;

        public float maxShield = 10;
        public float currentShield;
        
        public int stunTimer = 3;
        public bool shieldRecover;
        public bool shieldDestroy;

        protected override void Start(){
            base.Start();
            //_effectToSpawn = vfx[0];
            shieldRecover = true;
            currentShield = maxShield;
            HealthBar(10);
            _enemiesProjectile = effectToSpawn.GetComponent<EnemiesProjectile>();
        }

        protected override void Update(){
            ShieldRecover();
            base.Update();
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
                Instantiate(effectToSpawn, transform.position, rotation);
            }
        }

        protected override void HealthBar(int dmg){
            switch (shieldDestroy){
                case true: 
                    base.HealthBar(dmg);
                    return;
                case false:
                    currentShield -= dmg;
                    if (currentShield <= 0){
                        shieldDestroy = true;
                        rb3d.AddForce(25, 0, 25, ForceMode.Impulse);
                        StartCoroutine(WaitForStaggerToEnd());
                    }
                    return;
            }
        }

        private IEnumerator WaitForStaggerToEnd(){
            yield return new WaitForSeconds(stunTimer);
            shieldRecover = false;
        }

        private void ShieldRecover(){
            if (shieldRecover) return;
            currentShield += 2 * Time.deltaTime;
            
            if (currentShield >= maxShield){
                shieldRecover = true;
                shieldDestroy = false;
            }
        }
    }
}
