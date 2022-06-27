using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_FSM : MonoBehaviour
{
    public enum BossMode { BOSS1, BOSS2 }

    [SerializeField] private BossMode bossType;
    [SerializeField] private Transform target;
    public float inStateTimer;
    public float rotationSpeed;

    [Header("Melee Settings")]
    [SerializeField] private List<GameObject> meleeHitbox;
    public float chaseMinDistance;
    public float chaseTimeout = 5f;
    public float attackDistOffset = 0.5f;
    public int speed;
    public bool isAttacking;
    public bool isAttackFin;

    [Header("Range Settings")]
    public List<Transform> aimDirection;
    public List<GameObject> shootPrefab;
    public int shootCount;
    public float shootInterval = 3f;

    [Header("Default Settings")]
    public float restTimeout = 3f;
    public float stuntTimeout = 5f;

    [Header("Boss Value Configuration")]
    public List<float> value;

    private Boss_BaseState _currentState;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody rb;

    public Transform Target => target;
    public NavMeshAgent Agent => _agent;
    public Animator Anim => _animator;
    public Rigidbody Rb { get { return rb; } set { rb = value; } }

    //assign default as boss 1, or can go for null check 
    public Boss_Move1 move1State = new Boss1_SlicingClaws();
    public Boss_Move2 move2State = new Boss1_ThrustHand();
    public Boss_Move3 move3State = new Boss1_SlamGround();
    public Boss_Move4 move4State = new Boss1_Dash();
    public readonly Boss_Rest restState = new Boss_Rest();
    public readonly Boss_Stunt stuntState = new Boss_Stunt();
    public readonly Boss_Die dieState = new Boss_Die();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        target = PlayerHandler.Instance.transform;
        
        _agent.speed = speed;
        DoBossAttack(bossType);
        SetState(restState); 
    }

    private void Update()
    {
        _currentState.Update(this);
    }

    public void SetState(Boss_BaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void BossRandomState()
    {
        int randNum = Random.Range(0, 5);
      //  randNum = 4;
        switch (randNum)
        {
            case 0: SetState(restState); break;
            case 1: SetState(move1State); break;
            case 2: SetState(move2State); break;
            case 3: SetState(move3State); break;
            case 4: SetState(move4State); break;
        }
    }

    public int RandomChoice()
    {
        return Random.Range(0, 2);
    }

    /// <summary>
    /// Switch Boss State Machine Through enum bossType
    /// </summary>
    /// <param name="mode"></param>
    private void DoBossAttack(BossMode mode)
    {
        switch (mode)
        {
            case BossMode.BOSS1: 
                move1State = new Boss1_SlicingClaws();
                move2State = new Boss1_ThrustHand();
                move3State = new Boss1_SlamGround();
                move4State = new Boss1_Dash();
                break;
            case BossMode.BOSS2: 
                move1State = new Boss2_Attack(); 
                break;
        }

        //! This will invoke the Update of whatever class it morphed into
        move1State.Update(this);
        move2State.Update(this);
        move3State.Update(this);
        move4State.Update(this);
    }

    public void HitBoxOn(int i)
    {
        meleeHitbox[i].SetActive(true);
    }
    
    public void HitBoxOff(int i)
    {
        isAttackFin = true;
        meleeHitbox[i].SetActive(false);
    }

    public void ShootProjectile(GameObject shootPb, Transform aimer)
    {
        Vector3 targetDirection = (target.position - aimer.position).normalized;
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        aimer.eulerAngles = new Vector3(0, angle - 90, 0);
        GameObject bullet = Instantiate(shootPb, aimer.position, aimer.rotation);
        
        if (bullet.GetComponent<Projectile>() != null)
            bullet.GetComponent<Projectile>().SetDirection(target);
    }
    
    public void ShootProjectile2(GameObject shootPb, Transform aimer)
    {
        GameObject bullet = Instantiate(shootPb, aimer.position, transform.rotation);
    }

    public void RotateTowards(Transform target, Boss_FSM boss)
    {
        Vector3 direction = (target.position - boss.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, lookRotation, Time.deltaTime * boss.rotationSpeed);
    }
}
