using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Boss_FSM : MonoBehaviour
{
    private enum BossAttackMode { BOSS1, BOSS2 }
    public float inStateTimer;
    public float speed;

    [Header("Chase Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private BossAttackMode bam;
    public float chaseMinDistance;
    public float chaseTimeout = 5f;

    [Header("Melee Settings")] 
    [SerializeField] private GameObject meleeHitbox;
    public bool attacked;
    public LayerMask enemyLayer;

    [Header("Rest Settings")] 
    public float restTimeout = 3f;

    private Boss_BaseState _currentState;
    private NavMeshAgent _agent;
    private Animator _animator;

    public Transform Target { get { return target; } }
    public NavMeshAgent Agent { get { return _agent; } }
    public Animator Anim { get { return _animator; }}

    //assign default as boss 1, or can go for null check 
    public Boss_AttackState attackState = new Boss1_Attack();
    public readonly Boss_Chase chaseState = new Boss_Chase();
    public readonly Boss_Rest restState = new Boss_Rest();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _agent.speed = speed;
        DoBossAttack(bam);
        SetState(chaseState); 
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

    }

    /// <summary>
    /// Choose Boss Attack Mode
    /// </summary>
    /// <param name="attackMode"></param>
    private void DoBossAttack(BossAttackMode attackMode)
    {
        switch (attackMode)
        {
            case BossAttackMode.BOSS1: attackState = new Boss1_Attack(); break;
            case BossAttackMode.BOSS2: attackState = new Boss2_Attack(); break;
        }
        //! This will invoke the Update of whatever class it morphed into
        attackState.Update(this);
    }

    public void HitBoxOn()
    {
        attacked = true;
        meleeHitbox.SetActive(true);
    }
    
    public void HitBoxOff()
    {
        meleeHitbox.SetActive(false);
    }
}
