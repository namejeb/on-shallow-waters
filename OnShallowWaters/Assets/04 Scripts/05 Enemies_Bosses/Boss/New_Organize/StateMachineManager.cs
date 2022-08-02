using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using NaughtyAttributes;

public class StateMachineManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    public int speed;
    public float inStateTimer;
    public float rotationSpeed;
    public float faceAngle;
    public bool startBattle;
    public List<State> stateList;
    public List<State> passiveStates;

    float velocity = 100;

    [Header("Melee Settings")]
    [SerializeField] private List<GameObject> meleeHitbox;
    public float chaseMinDistance;
    public bool isAttacking;
    public bool isAttackFin;

    [Header("Range Settings")]
    public List<Transform> aimDirection;
    public List<GameObject> shootPrefab;

    [Header("Audio Settings")]
    [SerializeField] private SoundData bossSFX;

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
    public State CurrentState { get { return _currentState; } }

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
        SetState(passiveStates[2]);
    }

    private void OnEnable()
    {
        DialogueManager.OnDialogueEnd += StartBattle;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueEnd -= StartBattle;
    }

    void Update()
    {
        _currentState.UpdateState(this);

        if (transform.rotation.eulerAngles.y > (faceAngle + 5))
        {
            faceAngle = transform.rotation.eulerAngles.y;
            float currentWeight = _animator.GetLayerWeight(1);
            _animator.SetLayerWeight(1, Mathf.SmoothDamp(currentWeight, 1, ref velocity, 0.1f));
        }

        else if (transform.rotation.eulerAngles.y < (faceAngle - 5))
        {
            faceAngle = transform.rotation.eulerAngles.y;
            float currentWeight = _animator.GetLayerWeight(1);
            _animator.SetLayerWeight(1, Mathf.SmoothDamp(currentWeight, 1, ref velocity, 0.1f));
        }

        else if (transform.rotation.eulerAngles.y <= (faceAngle + 5) && transform.rotation.eulerAngles.y >= (faceAngle - 5))
        {
            float currentWeight = _animator.GetLayerWeight(1);
            _animator.SetLayerWeight(1, Mathf.SmoothDamp(currentWeight, 0, ref velocity, 0.3f));
        }
 
    }

    public void SetState(State state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void StartBattle()
    {
        if (_currentState == passiveStates[2])
        {
            startBattle = true;
        }
    }

    public void BossRandomState()
    {
        int randNum = Random.Range(0, stateList.Count);

        while (_currentState == stateList[randNum])
        {
            randNum = Random.Range(0, stateList.Count);
        }

        while (_currentState == stateList[1] && randNum == 4)
        {
            int[] num = { 0, 2, 3 };
            randNum = Random.Range(0, num.Length);
            randNum = num[randNum];
            Debug.Log(randNum);
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

    public void PlaySFX(string soundName)
    {
        SoundManager.instance.PlaySFX(bossSFX, soundName);
    }

    [Button]
    public void Shake()
    {
        impSource.GenerateImpulse();
    }

    [Button]
    public void Shoot()
    {
        SetState(stateList[3]);
    }

    [Button]
    public void Dash()
    {
        SetState(stateList[1]);
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
}
