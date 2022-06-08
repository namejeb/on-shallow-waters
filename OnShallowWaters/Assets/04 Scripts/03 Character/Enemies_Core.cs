using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemies_Core : MonoBehaviour{
    public NavMeshAgent agent; 
    protected private Transform puppet;

    private float _dist;
    private float attack_range = 5;

    public int core_health;
    public float core_speed;
    public float core_damage;

    protected private enum core_stage{
        idle, Move, Attack, SpecialMove, Dead
    } protected private core_stage behaviour;

    private void Start(){
        agent = GetComponent<NavMeshAgent>();
        puppet = GameObject.Find("Puppet").transform;
    }

    protected virtual void Update(){
        transform.LookAt(puppet);
        switch(behaviour){
            case core_stage.idle:
                break;
            case core_stage.Move:
                Movement();
                break;
            case core_stage.Attack:
                break;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    protected virtual void Movement(){
        //! Check Range //! Switch to Attack if inRange
        _dist = (puppet.position - transform.position).sqrMagnitude;

        agent.SetDestination(puppet.position);
        if(_dist < attack_range * attack_range){
            agent.stoppingDistance = attack_range;
            behaviour = core_stage.Attack;
        }
    }

    protected virtual void Attack(){
        //! Attack
    }

    protected virtual void HealthBar(int dmg){
        core_health -= dmg;
        
        if(core_health <= 0){
            //Death
            return;
        }
    }

    protected virtual void ReceivedBuff(){
        //! Check Certain Buff
    }

    protected virtual void SM(){
        print("This is Inheritance");
    }

    //! LA & HB Behaviours
    // Detect <> Follow <> Attack
    // Health <> Armour
    // Armour <> Recover

    //! FS & SHS Behaviours
    // Detect <> Position <> Line in Sight <> Attack

    //! BS & DS Behaviours
    // BS will heal, provide Extra Armour, increase Damage, Speed
    // DS will poison, decrease armour, decrease Speed and Skill Locking

    // Detect <> Position <> InRange <> Buff/Debuff (AOE)
    // Idle <> Move <> Execute Support

    //! Summoner Behaviours
    // Detect <> Position(Rarely) <> Line in Sight <> Attack
}
