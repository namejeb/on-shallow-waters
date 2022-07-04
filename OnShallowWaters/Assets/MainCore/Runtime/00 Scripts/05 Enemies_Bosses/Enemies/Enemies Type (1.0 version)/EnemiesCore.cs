using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public class EnemiesCore : MonoBehaviour {
        #region Basic Attributes
        [Space][Space]
        [Header("Basic Attributes: ")]
        public NavMeshAgent agent;
        public Transform puppet;
        public Rigidbody rb3d;
        public bool armourType;
        public Animator anim;
        private EnemyStats _enemyStats;

        //Enemies Detection & AttackRange
        [Space][Space]
        [Header("Detection & Attack Range: ")]
        [FormerlySerializedAs("_dist")] public float dist;
        public float detectRange = 10;
        public float attackRange = 5;
        
        //! Field of View
        [Space][Space]
        [Header("Field of View: ")]
        [Range(0, 30)]public float radius;
        [Range(0, 360)] public float angle;
        public LayerMask targetMask;
        public LayerMask obstructionMask;

        [Space][Space]
        [Header("Shield: ")]
        public float maxShield = 10;
        public float currentShield;
        
        public int stunTimer = 3;
        public bool shieldRecover;
        public bool shieldDestroy;
        
        //Enemies Stats
        [Space] [Space] 
        [Header("Stats: ")]
        public float maxHealth;
        private float _coreHealth;
        [FormerlySerializedAs("_coreSpeed")] public float coreSpeed;
        public float coreDamage;
        
        //Boon modifiers
        public static float shieldDmgBonus = 1f;

        #endregion

        private protected enum CoreStage{
            Idle, Move, Attack, SpecialMove
        } private protected CoreStage behaviour;

        #region Processing Field [Awake, Start, Update]

        protected virtual void Start(){
            agent = GetComponent<NavMeshAgent>();
            rb3d = GetComponent<Rigidbody>();
            puppet = GameObject.FindWithTag("Player").transform;
            
            _coreHealth = maxHealth;
            agent.speed = coreSpeed;
        }

        protected virtual void Update(){
            dist = (puppet.position - transform.position).sqrMagnitude;
            ShieldRecover();
            
            switch(behaviour){
                case CoreStage.Idle: Detection(); break;
                case CoreStage.Move: LookAtDirection(); Movement(); break;
                case CoreStage.Attack: LookAtDirection(); Attack(); break;
                case CoreStage.SpecialMove: UniqueMove(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Basic States
        protected virtual void Movement(){
            anim.SetBool("isWalk", true);
            agent.SetDestination(puppet.position);
            if(dist < attackRange * attackRange){
                agent.stoppingDistance = attackRange;
                behaviour = CoreStage.Attack;
            }
        }

        private void LookAtDirection(){
            var position = puppet.position;
            Vector3 rotation = new Vector3(position.x, transform.position.y, position.z);
            transform.LookAt(rotation);
        }

        protected virtual void Detection(){
            FieldOfViewCheck();
        }

        private void FieldOfViewCheck(){
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0) {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2){
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                        behaviour = CoreStage.Move;
                    }
                }
            }
        }

        protected virtual void Attack(){
            if(dist > attackRange * attackRange){
                behaviour = CoreStage.Move;
            }
        }

        protected virtual void UniqueMove(){
            //Temporary
        }
        #endregion
    
        #region Basic Buff, Health and Damage

        public virtual void ReceivedBuff(){
            if (_coreHealth + maxHealth * 0.2f < maxHealth){
                _coreHealth += maxHealth * 0.2f;
                return;
            }

            int buff = Random.Range(0,2);
            switch (buff){
                case 0 when coreSpeed < 10: agent.speed = coreSpeed *= 0.2f; return;
                case 1 when coreDamage < 30: coreDamage *= 0.2f; return;
            }
        }

        // protected virtual void HealthBar(int dmg){
        //     _coreHealth -= dmg;
        //     Debug.Log("Enemy Health: " + _coreHealth);
        //     //Set Health UI
        //
        //     if (_coreHealth > 0) return;
        //     StartCoroutine(Death());
        // }

        // private IEnumerator Death()
        // {
        //     yield return new WaitForSeconds(3f);
        // }

        public void ShieldBar(int damage) {
            switch (shieldDestroy){
                case true: _enemyStats.Damage(damage); return;
                case false:
                    currentShield -= damage * shieldDmgBonus;
                    if (currentShield <= 0){
                        shieldDestroy = true;
                        rb3d.AddForce(25, 0, 25, ForceMode.Impulse);
                        StartCoroutine(WaitForStaggerToEnd());
                    }
                    return;
            }
        }
        
        private void ShieldRecover(){
            if (!armourType) return;
            if (shieldRecover) return;
            currentShield += 2 * Time.deltaTime;

            if (!(currentShield >= maxShield)) return;
            shieldRecover = true;
            shieldDestroy = false;
        }
        
        private IEnumerator WaitForStaggerToEnd(){
            yield return new WaitForSeconds(stunTimer);
            shieldRecover = false;
        }

        // public void Damage(int damageAmount){
        //     switch (armourType){
        //         case false: _enemyStats.Damage(damageAmount); break;
        //         case true: ShieldBar(damageAmount); break;
        //     }
        // }
        
        //
        // public float LostHP()
        // {
        //     return maxHealth - _coreHealth;
        // }
        #endregion
    }
}
