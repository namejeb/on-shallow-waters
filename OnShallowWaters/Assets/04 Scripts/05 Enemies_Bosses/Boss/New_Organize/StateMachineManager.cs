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
    public List<GameObject> MH { get { return meleeHitbox; } set { meleeHitbox = value; } }


    public float faceAngle;

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
        faceAngle = transform.rotation.eulerAngles.y;
        _agent.speed = speed;
        SetState(stateList[0]);
    }


    float velocity = 10;
    void Update()
    {
        _currentState.UpdateState(this);

        if (transform.rotation.eulerAngles.y > (faceAngle + 5))
        {
            faceAngle = transform.rotation.eulerAngles.y;// -5;
            //_animator.SetLayerWeight(1, 1);
            float currentWeight = _animator.GetLayerWeight(1);
            _animator.SetLayerWeight(1, Mathf.SmoothDamp(currentWeight, 1, ref velocity, 0.1f));
        }

        else if (transform.rotation.eulerAngles.y < (faceAngle - 5))
        {
            faceAngle = transform.rotation.eulerAngles.y;// +5;
            //_animator.SetLayerWeight(1, 1);
            float currentWeight = _animator.GetLayerWeight(1);
            _animator.SetLayerWeight(1, Mathf.SmoothDamp(currentWeight, 1, ref velocity, 0.1f));
        }

        else if (transform.rotation.eulerAngles.y <= (faceAngle + 5) && transform.rotation.eulerAngles.y >= (faceAngle - 5))
        {
            //_animator.SetLayerWeight(1, 0);
            float currentWeight = _animator.GetLayerWeight(1);
            _animator.SetLayerWeight(1, Mathf.SmoothDamp(currentWeight, 0, ref velocity, 0.2f));
        }
 
    }

    public void SetState(State state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    [Button]
    public void Shoot()
    {
        SetState(stateList[3]);
    }

    [Button]
    public void Slam()
    {
        SetState(stateList[4]);
    }

    [Button]
    public void Slice()
    {
        SetState(stateList[2]);
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
    public void Shake()
    {
        float time = 2;
        //impSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = time;
        //impSource.m_DefaultVelocity.x = impSource.m_DefaultVelocity.y = -0.5f;
        impSource.GenerateImpulse();
    }
}
