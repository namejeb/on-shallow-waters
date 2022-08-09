using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public class MeleeDamage : MonoBehaviour {
        public int attackDamage;
        private bool _isAttackOn;
        private bool _targetHit;
        public GameObject enemy;

        private void OnEnable() {
            _isAttackOn = true;
            _targetHit = false;
        }

        private void OnDisable() {
            if (_targetHit) return;
            enemy.GetComponent<LightAttacker>().missAttack = true;
        }

        private void OnTriggerEnter(Collider col){
            if (!col.CompareTag("Player")) return;
            enemy.GetComponent<LightAttacker>().targetHit += 1;
            col.GetComponent<PlayerStats>().Damage(attackDamage);
            _isAttackOn = false;
            _targetHit = true;
        }
    }
}
