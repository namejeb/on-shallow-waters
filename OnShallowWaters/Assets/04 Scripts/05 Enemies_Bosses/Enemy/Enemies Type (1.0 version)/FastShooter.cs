using System;
using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public sealed class FastShooter : EnemiesCore {
        [SerializeField] private GameObject projectile;
        [SerializeField] private float repositioning, timeToFire;
        
        private EnemiesProjectile _enemiesProjectile;
        private Transform _transform2;
        
        private Quaternion _rotation;
        private Vector3 _position, _direct;

        public Transform fireSpawn;
        public float maxDist = 5f;
        public bool isPrepared;
        public int bulletFired;
        
        //! Walk Circle
        [SerializeField] private float rotationRadius = 2f, angularSpeed = 2f;
        public float posX, posZ, angle1;

        private Vector3 _offset;

        protected override void Start(){
            base.Start();
            isPrepared = true;
            _enemiesProjectile = projectile.GetComponent<EnemiesProjectile>();
        }

        protected override void Movement(){
            if (RaycastSingle() && isPrepared){
                behaviour = CoreStage.Attack;
                isPrepared = false;
            } else {
                RotateMovement();
            }
        }

        private IEnumerator Delay(){
            yield return new WaitForSeconds(repositioning);
            isPrepared = true;
        }

        protected override void Attack(){ 
            (_transform2 = transform).LookAt(puppet);
            
            _direct = puppet.position - _transform2.position;
            _rotation = Quaternion.LookRotation(_direct);
            Quaternion rotation1 = _rotation;
            
            if (Time.time >= timeToFire) {
                timeToFire = Time.time + 1 / _enemiesProjectile.fireRate;
                Instantiate(projectile, fireSpawn.position, rotation1);
                bulletFired += 1;
            }

            if (bulletFired != 3) return;
            behaviour = CoreStage.Move;
            bulletFired = 0;
        }

        private bool RaycastSingle(){
            var trans1 = transform;
            Vector3 origin = trans1.position;
            Vector3 direction = trans1.forward;

            Ray ray = new Ray(origin, direction);
            Debug.DrawRay(origin, direction * radius, Color.cyan);
            
            bool result = Physics.Raycast(ray, radius, targetMask, QueryTriggerInteraction.Ignore);
            return result;
        }

        private void RotateMovement(){
            var position13 = puppet.position;

            _offset.Set(
                Mathf.Cos(angle1) * radius,
                0,
                Mathf.Sin(angle1) * radius
            );
            
            agent.SetDestination(position13 + _offset);
            angle1 += Time.deltaTime * angularSpeed;
            StartCoroutine(Delay());

            if (angle >= 360f){
                angle = 0f;
            }
        }
    }
}
