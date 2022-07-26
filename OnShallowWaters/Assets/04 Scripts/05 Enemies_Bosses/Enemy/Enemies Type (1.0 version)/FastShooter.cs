using System.Collections;
using UnityEngine;


public sealed class FastShooter : EnemiesCore {
        [Header("Basic Attributes: ")]
        [SerializeField] private GameObject projectile;
        private float repositioning;
        private float timeToFire;
        
        [Space][Space]
        [Header("Fire Attributes: ")]
        private EnemiesProjectile _enemiesProjectile;
        private Transform transRecord;
        private Quaternion _rotation;
        private Vector3 _position, _direct;

        public Transform fireSpawn;
        private bool isPrepared;
        private int bulletFired;
        
        [Space]
        [Header("Orbit Rotation: ")]
        [SerializeField] private float rotationRadius = 2f, angularSpeed = 2f;
        private float posX, posZ, angle1;
        private Vector3 _offset;
        public float randomRadius;


        protected override void Start(){
            base.Start();
            isPrepared = true;
            _enemiesProjectile = projectile.GetComponent<EnemiesProjectile>();
            randomRadius = radius;
        }

        protected override void Movement(){
            if (RaycastSingle() && isPrepared){
                behaviour = CoreStage.Attack;
                isPrepared = false;
            } else {
                RotateMovement();
            }
        }

        protected override void Attack(){ 
            (transRecord = transform).LookAt(puppet);
            
            _direct = puppet.position - transRecord.position;
            _rotation = Quaternion.LookRotation(_direct);
            Quaternion rotation1 = _rotation;
            
            if (Time.time >= timeToFire) {
                timeToFire = Time.time + 1 / _enemiesProjectile.fireRate;
                Instantiate(projectile, fireSpawn.position, rotation1);
                bulletFired += 1;
            }

            if (bulletFired != 3) return;
            behaviour = CoreStage.Move;
            randomRadius = Random.Range(radius, radius + 5);
            bulletFired = 0;
        }

        private IEnumerator Delay(){
            yield return new WaitForSeconds(repositioning);
            isPrepared = true;
        }

        private bool RaycastSingle(){
            var trans1 = transform;
            Vector3 origin = trans1.position;
            Vector3 direction = trans1.forward;

            //Here
            Debug.DrawRay(origin, direction * randomRadius, Color.cyan);
            Ray ray = new Ray(origin, direction);

            //Here
            bool result = Physics.Raycast(ray, randomRadius, targetMask, QueryTriggerInteraction.Ignore);
            return result;
        }

        private void RotateMovement(){
            var position13 = puppet.position;

            //Here
            _offset.Set(
                Mathf.Cos(angle1) * randomRadius,
                0,
                Mathf.Sin(angle1) * randomRadius
            );
            
            agent.SetDestination(position13 + _offset);
            angle1 += Time.deltaTime * angularSpeed;
            StartCoroutine(Delay());

            if (angle >= 360f){
                angle = 0f;
            }
        }
    }
