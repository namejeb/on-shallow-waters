using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public class Buffer : EnemiesCore{
        [SerializeField] private float r;
        public LayerMask allyMask;
        private bool _once;

        protected override void Attack(){
            
        }

        private void AreaBuff() {
            Collider[] c = Physics.OverlapSphere(transform.position, r, allyMask);

            foreach (Collider nearbyAllies in c){
                if (!nearbyAllies.CompareTag("Enemy")) break;
                nearbyAllies.GetComponent<EnemiesCore>().ReceivedBuff();
            }
        }
    }
}