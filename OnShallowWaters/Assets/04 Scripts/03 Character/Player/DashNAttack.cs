using UnityEngine;
using _04_Scripts._05_Enemies_Bosses;
using System;
using System.Collections.Generic;
using UnityEngine.Timeline;
using System.Collections;
using System.Net;
using Unity.VisualScripting;

public class DashNAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private EnemyStats eStats;
    
    [SerializeField] private float dashDuration = 3f;
    [SerializeField] private float range;
    [SerializeField] private float speed = 5f;

    //Heavy Attack Variables
    [SerializeField] private float chargedTimer;
    public bool isSlash;
    public bool isSlam;

    private bool _isDash = false;
    
    private float _elapsedTime = 0f;
    private float _endTime = 0f;

    [SerializeField] private LayerMask damageableLayer;

    [SerializeField] private int attackSequence = 0;
    [SerializeField] private float nextAttack = 0;
    public float tempOutDamage = 0f;
    [SerializeField] public int outDamage;
    [SerializeField] private int inDamage;
    [SerializeField] private AttackButtonUI pressedButton;
    [SerializeField] private bool isSlashTigger;

    private SkBlessing _skBlessing;

    [SerializeField] private Transform boonEffectsManagerTransform;
    
    private List<Boon_Attack> _boonAttackList = new List<Boon_Attack>();
    private BM_DmgWhenArmorBreak _dmgWhenShieldBreak;

    //Tutorial Event
    public static event Action OnAttack;
    public static event Action OnDash;

    private bool _canAttack = true;
    
    private void InitBoonRefs()
    {
        Transform b = boonEffectsManagerTransform;
        
        // if is BA, add to list
        _boonAttackList.Add( b.GetComponent<BA_SingleEnemyDmgBonus>() );
        _boonAttackList.Add(b.GetComponent<BA_FirstTimeDmgBonus>());
        
        _dmgWhenShieldBreak = b.GetComponent<BM_DmgWhenArmorBreak>();
    }


    public static event Action OnCrit;
    
    private void Awake()
    {
        _skBlessing = GetComponent<SkBlessing>();
        InitBoonRefs();
    }

    private void Start()
    {
        stats = PlayerHandler.Instance.PlayerStats;
    }

    private void FixedUpdate()
    {
        if (_isDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        animator.SetTrigger("Dash");
        if (OnDash != null){
            OnDash();
        }

        playerMovement.Move(transform.forward, speed, true);
        if (Time.time > _endTime)
        {
            _isDash = false;
            playerMovement.enabled = true;
        }
    }

    public void ActivateDash()
    {
        _isDash = true;
        playerMovement.enabled = false;
        
        _endTime = Time.time + dashDuration * Time.timeScale;       //multiply timeScale to account for SlowMo 
    }

    public void Update()
    {
        
        if(pressedButton.isPressed)
           chargedTimer += Time.deltaTime;

        //calcualtion for charge attacks here

        if (pressedButton.isPressed == false)
        {
            if (chargedTimer >= 0.5f && chargedTimer < 1)
            {

                isSlash = true;
                if (isSlash)
                    HeavySlash();
            }

            else if (chargedTimer >= 1)
            {
                isSlam = true;
                if (isSlam)
                    HeavySlam();
            }
            else if (chargedTimer >= 0.01)
            {
                //detect normal
                Attack();
            }

            chargedTimer = 0;
        }
            
       
    }
    public void HeavySlash()
    {
        isSlash = false;
        float baseAtk = (float)stats.Atk.CurrentValue;
        float atkPercent = (float)stats.AtkPercent;
        float tempOutDamage = 0f;
        tempOutDamage = (float)(130f / 100f) * ((baseAtk + 0) * atkPercent);
        HandleDamaging(tempOutDamage);
        playerMovement.enabled = false;
        animator.SetTrigger("slashATK");
        attackSequence = 0;
        StartCoroutine(EnableMove(0.8f));
    }

    public void HeavySlam()
    {
        isSlam = false;
        float baseAtk = (float)stats.Atk.CurrentValue;
        float atkPercent = (float)stats.AtkPercent;
        float tempOutDamage = 0f;
        tempOutDamage = (float)(150f / 100f) * ((baseAtk + 0) * atkPercent);
        HandleDamaging(tempOutDamage);
        playerMovement.enabled = false;
        animator.SetTrigger("slamATK");
        attackSequence = 0;
        StartCoroutine(EnableMove(0.8f));
    }


    public void Attack()
    {
        //playerMovement.enabled = false;
        
        //Attack Sequence(What attack/aniamtion it will do)

        if (Time.time <= nextAttack) return;
        if (!_canAttack) return;
        
        float baseAtk = (float) stats.Atk.CurrentValue;
        float atkPercent = (float) stats.AtkPercent;

        float timeTillNextAtk = 0f;


        if ( attackSequence == 0 )
        {
            tempOutDamage = (float) (80f / 100f) * ((baseAtk + 0) * atkPercent);
            
            playerMovement.enabled = true;
            animator.SetTrigger("Attack");
            
            attackSequence++;
            timeTillNextAtk = .5f;
        }
        else if ( attackSequence == 1 )
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) ;

            playerMovement.enabled = true;
            animator.SetTrigger("Attack2");
            
            attackSequence++;
            timeTillNextAtk = .8f;
        }
        else if ( attackSequence == 2 )
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) ;

            playerMovement.enabled = true;
            animator.SetTrigger("Attack3");
            
            attackSequence = 0;
            timeTillNextAtk = 1f;
        }
        
        nextAttack =  (Time.time + timeTillNextAtk) / stats.AtkSpeed;
        
        StartCoroutine(EnableMove(timeTillNextAtk / stats.AtkSpeed));
        StartCoroutine(EnableAttack(timeTillNextAtk / stats.AtkSpeed));
        
        outDamage = Mathf.RoundToInt(tempOutDamage);
    }


    public void HandleDamaging(Collider hitObject)
    {
        IDamageable damagable = hitObject.GetComponent<IDamageable>();
        if (damagable == null) return;
        
        EnemyHandler enemyHandler = null;
        if (hitObject.CompareTag("Enemy"))
        {
            enemyHandler = hitObject.GetComponent<EnemyHandler>();
            if (enemyHandler != null) // if still null, meaning its a boss
            {
                outDamage = (int) HandleBoonDmgModifications( outDamage, enemyHandler);
            }
            _skBlessing.AddSoul(2);
        }
        // outDamage = ApplyCrit(outDamage);
        damagable.Damage( outDamage);
        
        if (enemyHandler == null) return;
        if ( _dmgWhenShieldBreak.Activated && enemyHandler.EnemiesCore != null)
        {
            _dmgWhenShieldBreak.ApplyEffect(enemyHandler);
        }
    }

      public void HandleDamaging(float outDamage)
     {
         Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f, damageableLayer);
         
         for (int i = 0; i < hitColliders.Length; i++)
         {
             // Debug.Log(hitColliders[i].gameObject.name);
             if (hitColliders[i] == null) continue;  //skip if null
             
             IDamageable damagable = hitColliders[i].GetComponent<IDamageable>();
             if (damagable == null) continue;
    
             //if hit an enemy
             EnemyHandler enemyHandler = null;
             if (hitColliders[i].CompareTag("Enemy"))
             {
                 enemyHandler = hitColliders[i].GetComponent<EnemyHandler>();
                 if (enemyHandler != null) // if still null, meaning its a boss
                 {
                     outDamage = HandleBoonDmgModifications(outDamage, enemyHandler);
                 }
                 _skBlessing.AddSoul(2);
             }
             
             //outDamage = ApplyCrit(outDamage);
             damagable.Damage( (int) outDamage);
            
             if (enemyHandler == null) continue;
             if ( _dmgWhenShieldBreak.Activated && enemyHandler.EnemiesCore != null)
             {
                 _dmgWhenShieldBreak.ApplyEffect(enemyHandler);
             }
         }
    }

    public float HandleBoonDmgModifications(float outDamage, EnemyHandler e)
    {
        for (int j = 0; j < _boonAttackList.Count; j++)
        { 
            Boon_Attack b = _boonAttackList[j];
            if (b.Activated)
            {
                if (b.Type == DmgModificationType.DMG_ONLY)
                    outDamage = b.ApplyEffect(outDamage);
                else if (b.Type == DmgModificationType.DMG_ENEMY)
                    outDamage = b.ApplyEffect(outDamage, e);
            }
        }

        return outDamage;
    }

    private float ApplyCrit(float outgoingDamage)
    {
        float cr = UnityEngine.Random.Range(0f, 1f);
        if (cr < stats.CritChance)
        {
            outgoingDamage *= stats.CritDamage;
            if(OnCrit != null) OnCrit.Invoke();
        }

        return outgoingDamage;
    }

    private IEnumerator EnableMove(float timer)
    {
        yield return new WaitForSeconds(timer);
        playerMovement.enabled = true;
    }
    
    private IEnumerator EnableAttack(float duration)
    {
        _canAttack = false;
        
        yield return new WaitForSeconds(duration);
        _canAttack = true;
    }


}
