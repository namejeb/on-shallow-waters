
using UnityEngine;


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

