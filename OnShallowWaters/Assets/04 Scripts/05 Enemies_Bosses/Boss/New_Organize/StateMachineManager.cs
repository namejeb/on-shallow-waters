using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachineManager : MonoBehaviour
{
    public enum BossMode { BOSS1, BOSS2 }

    [SerializeField] private BossMode bossType;
    [SerializeField] private Transform target;
    public int speed;
    public float inStateTimer;
    public float rotationSpeed;
    public List<State> stateList;
    public List<State> passiveStates;

    [Header("Melee Settings")]
    [SerializeField] private List<GameObject> meleeHitbox;
    public float chaseMinDistance;
    public bool isAttacking;
    public bool isAttackFin;

    [Header("Range Settings")]
    public List<Transform> aimDirection;
    public List<GameObject> shootPrefab;

    private State _currentState;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Rigidbody rb;

    public Transform Target => target;
    public NavMeshAgent Agent => _agent;
    public Animator Anim => _animator;
    public Rigidbody Rb { get { return rb; } set { rb = value; } }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        target = PlayerHandler.Instance.transform;

        Agent.stoppingDistance = chaseMinDistance;

        _agent.speed = speed;
        //DoBossAttack(bossType);
        SetState(stateList[0]);
    }

    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SetState(State state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void BossRandomState()
    {
        int randNum = Random.Range(0, stateList.Count);
        SetState(stateList[randNum]);
    }

    /// <summary>
    /// Switch Boss State Machine Through enum bossType
    /// </summary>
    /// <param name="mode"></param>
    //private void DoBossAttack(BossMode mode)
    //{
    //    switch (mode)
    //    {
    //        case BossMode.BOSS1:
    //            move1State = new Boss1_SlicingClaws();
    //            move2State = new Boss1_ThrustHand();
    //            move3State = new Boss1_SlamGround();
    //            move4State = new Boss1_Dash();
    //            break;
    //        case BossMode.BOSS2:
    //            move1State = new Boss2_Attack();
    //            break;
    //    }

    //    //! This will invoke the Update of whatever class it morphed into
    //    move1State.Update(this);
    //    move2State.Update(this);
    //    move3State.Update(this);
    //    move4State.Update(this);
    //}

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

    public void RotateTowards()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
