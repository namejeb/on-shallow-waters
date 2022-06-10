using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Boss_FSM : MonoBehaviour
{
    public enum BossMode { BOSS1, BOSS2 }
    public float inStateTimer;

    private List<Boss_BaseState> stateList;

    [Header("Chase Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private BossMode bam;
    public float chaseMinDistance;
    public float chaseTimeout = 5f;
    public float attackDistOffset = 1f;

    [Header("Melee Settings")] 
    [SerializeField] private GameObject meleeHitbox;
    public bool isAttacking;
    public bool isAttackFin;
    public int speed;
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
    public Boss_Rest restState = new Boss_Rest();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DoBossAttack(bam);
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

    public void BossRandomState(BossMode mode)
    {
        
    }

    private void BossStateList(BossMode mode)
    {
        switch(mode)
        {
            case BossMode.BOSS1:
                stateList.Add(attackState);
                stateList.Add(restState);
                break;
            case BossMode.BOSS2:
                break;
        }
        
    }

    /// <summary>
    /// Switch between Boss Mode
    /// </summary>
    /// <param name="mode"></param>
    private void DoBossAttack(BossMode mode)
    {
        switch (mode)
        {
            case BossMode.BOSS1: 
                attackState = new Boss1_Attack();
                break;
            case BossMode.BOSS2: 
                attackState = new Boss2_Attack(); 
                break;
        }
        
        
        
        //! This will invoke the Update of whatever class it morphed into
        attackState.Update(this);
    }

    public void HitBoxOn()
    {
        if (meleeHitbox == null)
        {
            Debug.LogWarning("MeleeHitbox not assisgned");
            return;
        }
        meleeHitbox.SetActive(true);
    }
    
    public void HitBoxOff()
    {
        if (meleeHitbox == null)
        {
            Debug.LogWarning("MeleeHitbox not assisgned");
            return;
        }
        isAttackFin = true;
        meleeHitbox.SetActive(false);
    }
}
