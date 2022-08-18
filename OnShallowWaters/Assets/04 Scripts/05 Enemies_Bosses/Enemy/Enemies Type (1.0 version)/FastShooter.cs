using System.Collections;
using UnityEngine;


public sealed class FastShooter : EnemiesCore {
        [Header("Basic Attributes: ")]
        [SerializeField] private GameObject projectile;
        [SerializeField] private float repositioning;
        private float timeToFire;
        
        [Space][Space]
        [Header("Fire Attributes: ")]
        private EnemiesProjectile _enemiesProjectile;
        private bool isPrepared;
        public int bulletFired;
        
        [Space]
        [Header("Orbit Rotation: ")]
        [SerializeField] private float rotationRadius = 2f, angularSpeed = 2f;
        private float posX, posZ, angle1;
        private Vector3 _offset;
        public float randomRadius;
        public SoundData crabShootSFX;
        protected override void Start(){
            base.Start();
            isPrepared = true;
            _enemiesProjectile = projectile.GetComponent<EnemiesProjectile>();
            randomRadius = radius;
        }

        protected override void Movement(){
            anim.SetBool("isWalk", true);
            
            if (RaycastSingle() && isPrepared){
                behaviour = CoreStage.Attack;
                isPrepared = false;
            } else {
                RotateMovement();
            }
        }

        protected override void Attack(){
            anim.SetBool("isWalk", false);
           
            if (Time.time >= timeToFire) {
            //print("Fire");
            //SoundManager.instance.PlaySFX(crabShootSFX, "MiniCrabShoot");
            anim.SetTrigger("isAttack2"); 
           
                timeToFire = Time.time + 1 / _enemiesProjectile.fireRate;
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
            Transform trans1 = transform;
            Vector3 origin = trans1.position + new Vector3(0, 1, 0);
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

            _offset.Set(
                Mathf.Cos(angle1) * randomRadius,
                0,
                Mathf.Sin(angle1) * randomRadius
            );
            
            agent.SetDestination(position13 + _offset);
            angle1 += Time.deltaTime * angularSpeed;
            StartCoroutine(Delay());

            if (angle1 >= 360f){
                angle1 = 0f;
            }
        }

        private void OnDisable(){
            bulletFired = 0;
            isPrepared = true;
        }
    }
