using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

    public class EnemiesCore : MonoBehaviour {
        #region Basic Attributes
        [Space][Space]
        [Header("Basic Attributes: ")]
        public NavMeshAgent agent;
        public Transform puppet;
        public Rigidbody rb3d;
        public Animator anim;
        
        private EnemyStats _enemyStats;
        public bool armourType;
        
        [Space][Space]
        [Header("Detection & Attack Range: ")]
        [FormerlySerializedAs("_dist")] public float dist;
        public float attackRange = 5;
        public float detectRange;

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
            Idle, Move, Attack
        } private protected CoreStage behaviour;

        #region Processing Field [Awake, Start, Update]
        protected virtual void Start(){
            agent = GetComponent<NavMeshAgent>();
            rb3d = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            puppet = GameObject.FindWithTag("Player").transform;

            _enemyStats = GetComponent<EnemyStats>();
            detectRange = radius;
        }

        public bool one;

        protected virtual void Update(){
            dist = (puppet.position - transform.position).sqrMagnitude;
            ShieldRecover();
  
            //if(_enemyStats.isDead) return;
            switch(behaviour){
                case CoreStage.Idle: Detection(); break;
                case CoreStage.Move: LookAtDirection(); Movement(); break;
                case CoreStage.Attack: LookAtDirection(); Attack(); break;
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
        }
        #endregion
    
        #region Basic Buff, Health and Damage
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
        #endregion
    }

