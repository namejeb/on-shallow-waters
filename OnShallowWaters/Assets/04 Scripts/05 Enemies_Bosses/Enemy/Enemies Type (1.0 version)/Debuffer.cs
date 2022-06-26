
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_{
    public class Debuffer : EnemiesCore {
        public Transform firePoint;
        public bool fireOnce;
        public float maxDist = 5f;
        
        public float rotationSpeed = 1;
        public float throwPower = 5;
        
        //! Sebastian Lague Method for Kinematic Equation
        public GameObject bottle;
        
        private Vector3 _direction;
        private Quaternion _rotate;
        private Vector3 _pos;
        
        //public Rigidbody ball;
        public float height = 25;
        public float gravity = -18;
        
        protected override void Movement(){
            behaviour = CoreStage.Attack;
        }

        [SerializeField] private float timeToFire;
        //private BottleAoe _enemiesProjectile;
        // ball = bottle.GetComponent<Rigidbody>();
            // ball.useGravity = false;
            
            protected override void Attack(){
                Transform trans;
                (trans = transform).LookAt(puppet);
                _direction = puppet.position - trans.position;
                _rotate = Quaternion.LookRotation(_direction);
                
                //RaycastSingle();
                if (Time.time >= timeToFire) {
                    timeToFire = Time.time + 1 / 0.5f;
                    ThrowBottle();
                }
            }

            // private void RaycastSingle(){
            //      var transform1 = transform;
            //      var position = transform1.position;
            //     
            //     Vector3 origin = position;
            //     Vector3 direction = transform1.forward;
            //
            //     Vector3 directionA = puppet.position - position;
            //     float x = Random.Range(-5, 5);
            //     float y = Random.Range(-5, 5);
            //     Vector3 directionB = directionA + new Vector3(x, y, 0);
            //
            //     Ray ray = new Ray(origin, direction);
            //     bool result = Physics.Raycast(ray, out RaycastHit hitInfo, maxDist);
            //
            //     if (!result || !hitInfo.collider.CompareTag("Player") || fireOnce) return;
            //     fireOnce = true;
            //
            //     //Launch();
            // }
        
        private void ThrowBottle() {
            Quaternion rotation = _rotate;
            
            var position = puppet.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
                                                  new Vector3(0, position.y * rotationSpeed, position.z * rotationSpeed));
            
            GameObject debuffBottle = Instantiate(bottle, firePoint.position, rotation);
            //debuffBottle.GetComponent<Rigidbody>().velocity = firePoint.transform.up * throwPower;
            Physics.gravity = Vector3.up * gravity;
            debuffBottle.GetComponent<Rigidbody>().velocity = CalculateLaunchVelocity();
        }

        // void Launch(){
        //     Instantiate(bottle, firePoint.position, firePoint.rotation);
        //     Physics.gravity = Vector3.up * gravity;
        //     ball.useGravity = true;
        //     ball.velocity = CalculateLaunchVelocity();
        // }
        //
        // //! Sebastian Lague Method for Kinematic Equation
        Vector3 CalculateLaunchVelocity() {
            var position = puppet.position;
            var position1 = firePoint.position;
            
            float displacementY = position.y - position1.y;
            Vector3 displacementXZ =
                new Vector3(position.x - position1.x, 0, position.z - position1.z);
        
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
            Vector3 velocityXZ = displacementXZ /
                                 (Mathf.Sqrt(-2 * height / gravity) +
                                  Mathf.Sqrt(2 * (displacementY - height) / gravity));
        
            return velocityXZ + velocityY;
        }
    }
}
