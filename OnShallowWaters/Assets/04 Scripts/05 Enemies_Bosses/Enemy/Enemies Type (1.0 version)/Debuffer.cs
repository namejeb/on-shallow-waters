
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_{
    public class Debuffer : EnemiesCore {
        public float rotationSpeed = 2;
        public float throwPower = 5;
        
        public GameObject bottle;
        public Transform firePoint;
        public bool fireOnce;
        
        protected override void Attack(){
            RaycastSingle();
        }
        
        private void RaycastSingle(){
            var transform1 = transform;
            var position = transform1.position;
            
            Vector3 origin = position;
            Vector3 direction = transform1.forward;
            float maxDist = 5f;

            Vector3 directionA = puppet.position - position;
            float x = Random.Range(-5, 5);
            float y = Random.Range(-5, 5);
            Vector3 directionB = directionA + new Vector3(x, y, 0);

            Ray ray = new Ray(origin, direction);
            bool result = Physics.Raycast(ray, out RaycastHit hitInfo, maxDist);

            if (!result || !hitInfo.collider.CompareTag("Player") || fireOnce) return;
            fireOnce = true;
            ThrowBottle();
        }
        
        private void ThrowBottle() {
            var position = puppet.position;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
                                                  new Vector3(0, position.y * rotationSpeed, position.z * rotationSpeed));
            
            GameObject debuffBottle = Instantiate(bottle, firePoint.position, firePoint.rotation);
            debuffBottle.GetComponent<Rigidbody>().velocity = firePoint.transform.up * throwPower;
        }
    }
}
