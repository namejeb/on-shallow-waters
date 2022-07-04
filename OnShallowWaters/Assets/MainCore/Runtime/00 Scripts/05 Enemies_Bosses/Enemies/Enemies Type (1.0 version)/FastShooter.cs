using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public sealed class FastShooter : EnemiesCore{
        public GameObject projectile;
        public bool fireOnce;

        // agent.SetDestination(puppet.position);
        // if(dist < attackRange * attackRange){
        //     agent.stoppingDistance = attackRange;
        //     behaviour = CoreStage.Attack;
        // }
        
        protected override void Movement(){
        }

        protected override void Attack(){
            RaycastSingle();
        }

        private bool RaycastSingle(){
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

            if (!result || !hitInfo.collider.CompareTag("Player") || fireOnce) return false;
            GameObject currentBullet = Instantiate(projectile, transform.position, Quaternion.identity);
            currentBullet.transform.forward = directionB.normalized;
            fireOnce = true;
            return true;
        }
    }
}
