using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using NaughtyAttributes;

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
    private CinemachineImpulseSource impSource;

    public Transform Target => target;
    public NavMeshAgent Agent => _agent;
    public Animator Anim => _animator;
    public Rigidbody Rb { get { return rb; } set { rb = value; } }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        impSource = FindObjectOfType<CinemachineImpulseSource>();
    }

    private void Start()
    {
        target = PlayerHandler.Instance.transform;

        Agent.stoppingDistance = chaseMinDistance;

        _agent.speed = speed;
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

        while (_currentState == stateList[randNum])
        {
            randNum = Random.Range(0, stateList.Count);
        }

        SetState(stateList[randNum]);
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

    public void ShootProjectile(int index)
    {
        Vector3 targetDirection = (target.position - aimDirection[index].position).normalized;
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        aimDirection[index].eulerAngles = new Vector3(0, angle - 90, 0);
        GameObject bullet = Instantiate(shootPrefab[index], aimDirection[index].position, aimDirection[index].rotation);

        if (bullet.GetComponent<Projectile>() != null)
            bullet.GetComponent<Projectile>().SetDirection(target);
    }

    public void ShootProjectile2(int index)
    {
        GameObject bullet = Instantiate(shootPrefab[index], aimDirection[index].position, transform.rotation);
    }

    public void RotateTowards()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    [Button]
    private void Shake()
    {
        Debug.Log("Shake");
        float time = 2;
        impSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = time;
        impSource.m_DefaultVelocity.x = impSource.m_DefaultVelocity.y = -0.5f;
        impSource.GenerateImpulse();
    }
}
