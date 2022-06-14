using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _04_Scripts._05_Enemies_Bosses.Enemy {
    public class EnemiesCore : MonoBehaviour, IDamageable{
        #region Basic Attributes
        public NavMeshAgent agent;
        public Transform puppet;
        public Rigidbody rb3d;

        //Enemies Detection & AttackRange
        [FormerlySerializedAs("_dist")] public float dist;
        public float detectRange = 10;
        public float attackRange = 5;
    
        //Enemies Stats
        public int coreHealth;
        public float coreSpeed;
        public float coreDamage;
        #endregion

        private protected enum CoreStage{
            Idle, Move, Attack, SpecialMove
        } private protected CoreStage behaviour;

        #region Processing Field [Awake, Start, Update]
        protected virtual void Start(){
            agent = GetComponent<NavMeshAgent>();
            rb3d = GetComponent<Rigidbody>();
            puppet = GameObject.Find("Puppet").transform;
            agent.speed = coreSpeed;
        }

        protected virtual void Update(){
            dist = (puppet.position - transform.position).sqrMagnitude;
            switch(behaviour){
                case CoreStage.Idle: Detection(); break;
                case CoreStage.Move: transform.LookAt(puppet); Movement(); break;
                case CoreStage.Attack: Attack(); break;
                case CoreStage.SpecialMove: UniqueMove(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Basic States
        protected virtual void Movement(){
            agent.SetDestination(puppet.position);
            if(dist < attackRange * attackRange){
                agent.stoppingDistance = attackRange;
                behaviour = CoreStage.Attack;
            }
        }

        protected virtual void Detection(){
            if(dist < detectRange * detectRange){
                behaviour = CoreStage.Move;
            }
        }

        protected virtual void Attack(){
            if(dist > attackRange * attackRange){
                behaviour = CoreStage.Move;
            }
        }

        protected virtual void UniqueMove(){
            //For Unique Behaviour
        }
        #endregion
    
        #region Basic Buff, Health and Damage

        public virtual void ReceivedBuff(){
            int randomBuff = Random.Range(0, 3);
            switch (randomBuff){
                case 0: coreHealth += 20; break;
                case 1:
                    coreSpeed += 10;
                    agent.speed = coreSpeed;
                    break;
                case 2:
                    coreDamage += 20;
                    break;
            }
        }

        protected virtual void HealthBar(int dmg){
            coreHealth -= dmg;

            if(coreHealth <= 0){
                print("Dead");
            }
        }

        public void Damage(int damageAmount){
            HealthBar(damageAmount);
        }
        #endregion
    }
}
