using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Cinemachine;
using UnityEngine.UI;


public class DashNAttack : MonoBehaviour
{
    [Header("System:")]
    [SerializeField] private SoundData attkSFX;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Button dashButton;

    [SerializeField] private float dashDuration = 3f;
    [SerializeField] private float speed = 5f;

    // attack
    [SerializeField] private LayerMask damageableLayer;
    
    [SerializeField] private int attackSequence = 0;
    [SerializeField] private float nextAttack = 0;
    public float tempOutDamage = 0f;
    [SerializeField] public int outDamage;
    [SerializeField] private AttackButtonUI pressedButton;
    private bool _canAttack = true;
    [SerializeField] private float resetAttackTimer;

    
    //Heavy Attack Variables
    [SerializeField] private float chargedTimer;
    public bool isSlash;
    public bool isSlam;

    // Dash
    private bool _isDash = false;
    private float _endTime = 0f;

    // SKB
    private SkBlessing _skBlessing;

    // Boons
    [SerializeField] private Transform boonEffectsManagerTransform;
    
    private List<Boon_Attack> _boonAttackList = new List<Boon_Attack>();
    private BM_DmgWhenArmorBreak _dmgWhenShieldBreak;

    //Tutorial Event
    public static event Action OnAttack;
    public static event Action OnDash;
    
    public static event Action<Transform, float, bool> OnHitLanded;



    public LayerMask DamageableLayer { get => damageableLayer; }
    
    private void InitBoonRefs()
    {
        Transform b = boonEffectsManagerTransform;
        
        // if is BA, add to list
        _boonAttackList.Add( b.GetComponent<BA_SingleEnemyDmgBonus>() );
        _boonAttackList.Add(b.GetComponent<BA_FirstTimeDmgBonus>());
        
        _dmgWhenShieldBreak = b.GetComponent<BM_DmgWhenArmorBreak>();
    }

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
        StartCoroutine(EnableButton(2f));
    }

    public void ActivateDash()
    {

        dashButton.interactable = false;
        _isDash = true;
        playerMovement.enabled = false;
        
        _endTime = Time.time + dashDuration * Time.timeScale;
        //multiply timeScale to account for SlowMo 
        
    }

    public void Update()
    {
        if(pressedButton.isPressed)
           chargedTimer += Time.deltaTime;

        if(resetAttackTimer <= Time.time)
        {
            attackSequence = 0;
        }

        // calculation for charge attacks here

        // slash
        float slashTimerStart = .5f / stats.AtkSpeed;
        float slashTimerEnd = 1f / stats.AtkSpeed;
        
        // slam
        float slamTimerStart = 1f / stats.AtkSpeed;
  

        if (pressedButton.isPressed == false)
        {
            if (chargedTimer >= slashTimerStart && chargedTimer < slashTimerEnd)
            {
                isSlash = true;
                if (isSlash)
                    HeavySlash();
            }

            else if (chargedTimer >= slamTimerStart)
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
    private void HeavySlash()
    {
        isSlash = false;
        playerMovement.enabled = false;
        
        animator.SetTrigger("slashATK");
        
        // dmg calculation + application
        float baseAtk = (float)stats.Atk.CurrentValue;
        float atkPercent = (float)stats.AtkPercent;
        float tempOutDamage = 0f;
        tempOutDamage = (float)(170f / 100f) * ((baseAtk + 0) * atkPercent);

        Vector3 pos = transform.position + (transform.forward * 1.2f);
        StartCoroutine(HandleDamaging(tempOutDamage, 3.3f, .3f, pos));
        
        //attackSequence = 0;
        StartCoroutine(EnableMove(0.8f/stats.AtkSpeed));
    }

    private void HeavySlam()
    {
        isSlam = false;
        playerMovement.enabled = false;
        
        animator.SetTrigger("slamATK");
        
        // dmg calculation + application
        float baseAtk = stats.Atk.CurrentValue;
        float atkPercent = stats.AtkPercent;
        float tempOutDamage = 0f;
        tempOutDamage = (200f / 100f) * ((baseAtk + 0) * atkPercent);
        StartCoroutine(HandleDamaging(tempOutDamage, 4f, .65f, transform.position, true));
        
        //attackSequence = 0;
        StartCoroutine(EnableMove(0.8f / stats.AtkSpeed));
    }

    public void ShakeCamera()
    {
        impulseSource.GenerateImpulseWithForce(.3f);
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
            
            playerMovement.enabled = false;
            animator.SetTrigger("Attack");
            SoundManager.instance.PlaySFX(attkSFX, "Attack 1");
            
            attackSequence++;
            timeTillNextAtk = 1f;
            resetAttackTimer = Time.time + 2;
        }
        else if ( attackSequence == 1 )
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) ;

            playerMovement.enabled = false;
            animator.SetTrigger("Attack2");
            SoundManager.instance.PlaySFX(attkSFX, "Attack 2");
            attackSequence++;
            timeTillNextAtk = 1f;
            resetAttackTimer = Time.time + 2;
        }
        else if ( attackSequence == 2 )
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) ;

            playerMovement.enabled = false;
            animator.SetTrigger("Attack3");
            SoundManager.instance.PlaySFX(attkSFX, "Attack 3");
            attackSequence = 0;
            timeTillNextAtk = 1.2f;
            resetAttackTimer = Time.time + 2;
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
        
        bool isCrit = CheckIfCrit();
        if (isCrit)
        {
            outDamage = (int) ApplyCrit(outDamage);
        }

        outDamage = (int) damagable.GetReceivedDamage( outDamage);
        damagable.Damage( outDamage );
        
        // display damage text
        if(OnHitLanded != null) OnHitLanded.Invoke(hitObject.transform, outDamage, isCrit);
        
        if (enemyHandler == null) return;
        if ( _dmgWhenShieldBreak.Activated && enemyHandler.EnemiesCore != null)
        {
            _dmgWhenShieldBreak.ApplyEffect(enemyHandler);
        }
    }

    private IEnumerator HandleDamaging(float outDamage, float radius, float delay, Vector3 pos, bool shake = false)
    {
        yield return new WaitForSeconds(delay);
        
        if(shake)
            ShakeCamera();
        
        Collider[] hitColliders = Physics.OverlapSphere(pos, radius, damageableLayer);
         
        for (int i = 0; i < hitColliders.Length; i++)
        {
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

            bool isCrit = CheckIfCrit();
            if (isCrit)
            {
                outDamage = ApplyCrit(outDamage);
            }
            
            outDamage = (int) damagable.GetReceivedDamage( outDamage);
            damagable.Damage( (int) outDamage );
             
            // display damage text
            if(OnHitLanded != null) OnHitLanded.Invoke(hitColliders[i].transform, outDamage, isCrit);
            
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

    private bool CheckIfCrit()
    {
        float cr = UnityEngine.Random.Range(0f, 1f);
        if (cr < stats.CritChance) return true;
        return false;
    }

    private float ApplyCrit(float outgoingDamage)
    {
        outgoingDamage *= stats.CritDamage;
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

    private IEnumerator EnableButton(float timer)
    {
        yield return new WaitForSeconds(timer);
        dashButton.interactable = true;
    }


}
