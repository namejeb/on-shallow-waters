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
    [SerializeField] private GameObject chargingVFX;

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
    public static event Action OnHeavySlash;
    public static event Action OnHeavySlam;
    
    public static event Action OnDash;
    public static event Action<Transform, float, bool> OnHitLanded;

    public static event Action<Transform, VFXPickups.PickupType> OnSpawnPickup;



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
        if (OnDash != null){
            OnDash();
        }
        dashButton.interactable = false;
        _isDash = true;
        playerMovement.enabled = false;
        
        _endTime = Time.time + dashDuration * Time.timeScale;
        //multiply timeScale to account for SlowMo 
    }

    public void Update()
    {
        // calculation for charge attacks here

        // slash
        float slashTimerStart = .5f / stats.AtkSpeed;
        float slashTimerEnd = 1.2f / stats.AtkSpeed;

        // slam
        float slamTimerStart = 1.2f / stats.AtkSpeed;


        if (pressedButton.isPressed)
        {
            chargedTimer += Time.unscaledDeltaTime;
            if (chargedTimer >= slashTimerStart && chargedTimer < slashTimerEnd)
            {
                chargingVFX.SetActive(true);
            }
        }

        if(resetAttackTimer <= Time.unscaledTime)
        {
            attackSequence = 0;
        }

        if (pressedButton.isPressed == false)
        {
            chargingVFX.SetActive(false);

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
        playerMovement.canMove = false;
        
        animator.SetTrigger("slashATK");
        SoundManager.instance.PlaySFX(attkSFX, "Attack 4");
        // dmg calculation + application
        float baseAtk = (float)stats.Atk.CurrentValue;
        float atkPercent = (float)stats.AtkPercent;
        float tempOutDamage = 0f;
        tempOutDamage = (float)(170f / 100f) * ((baseAtk + 0) * atkPercent);

        Vector3 pos = transform.position + (transform.forward * 1.2f);
        StartCoroutine(HandleDamaging(tempOutDamage, 3.3f, .3f, pos));

        if(OnHeavySlash != null) OnHeavySlash.Invoke();
        
        StartCoroutine(EnableMove(1/stats.AtkSpeed));
    }

    private void HeavySlam()
    {
        isSlam = false;
        playerMovement.canMove = false;
        
        animator.SetTrigger("slamATK");
        SoundManager.instance.PlaySFX(attkSFX, "Attack 5");
        // dmg calculation + application
        float baseAtk = stats.Atk.CurrentValue;
        float atkPercent = stats.AtkPercent;
        float tempOutDamage = 0f;
        tempOutDamage = (200f / 100f) * ((baseAtk + 0) * atkPercent);
        StartCoroutine(HandleDamaging(tempOutDamage, 4f, .65f, transform.position, true));
        
        if(OnHeavySlam != null) OnHeavySlam.Invoke();
   
        StartCoroutine(EnableMove(1 / stats.AtkSpeed));
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
            
            playerMovement.canMove = false;
            animator.SetTrigger("Attack");
            SoundManager.instance.PlaySFX(attkSFX, "Attack 1");
            
            attackSequence++;
            timeTillNextAtk = .6f;
            resetAttackTimer = Time.unscaledTime + 2;
        }
        else if ( attackSequence == 1 )
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) ;

            playerMovement.canMove = false;
            animator.SetTrigger("Attack2");
            SoundManager.instance.PlaySFX(attkSFX, "Attack 2");
            attackSequence++;
            timeTillNextAtk = .8f;
            resetAttackTimer = Time.unscaledTime + 2;
        }
        else if ( attackSequence == 2 )
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) ;

            playerMovement.canMove = false;
            animator.SetTrigger("Attack3");
            SoundManager.instance.PlaySFX(attkSFX, "Attack 3");
            attackSequence = 0;
            timeTillNextAtk = 1f;
            resetAttackTimer = Time.unscaledTime + 2;
        }
        
        nextAttack =  (Time.unscaledTime + timeTillNextAtk) / stats.AtkSpeed;
        
        //StartCoroutine(EnableMove(timeTillNextAtk / stats.AtkSpeed));


        StartCoroutine(EnableAttack(timeTillNextAtk + 0.1f / stats.AtkSpeed));
        StartCoroutine(EnableMove(timeTillNextAtk / stats.AtkSpeed));

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
        else
        {
            if (!hitObject.CompareTag("TrainingDummy"))
            {
                HandleSpawnPickup(hitObject.transform);
            }
        }

        bool isCrit = CheckIfCrit();
        if (isCrit)
        {
            outDamage = (int) ApplyCrit(outDamage);
        }

        outDamage = (int) damagable.GetReceivedDamage( outDamage);
        damagable.Damage( outDamage );
        
        // display damage text
        Transform hitTransform = hitObject.transform;
        HandleDamageText(hitTransform, outDamage, isCrit);
        
        if (enemyHandler == null) return;
        if ( _dmgWhenShieldBreak.Activated && enemyHandler.EnemiesCore != null)
        {
            _dmgWhenShieldBreak.ApplyEffect(enemyHandler);
        }
        HandleSpawnPickup(hitTransform, VFXPickups.PickupType.Soul, enemyHandler);
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
            else
            {
                if (!hitColliders[i].CompareTag("TrainingDummy"))
                {
                    HandleSpawnPickup(hitColliders[i].transform);
                }
            }

            bool isCrit = CheckIfCrit();
            if (isCrit)
            {
                outDamage = ApplyCrit(outDamage);
            }
            
            outDamage = (int) damagable.GetReceivedDamage( outDamage);
            damagable.Damage( (int) outDamage );
             
            // display damage text
            Transform hitTransform = hitColliders[i].transform;
            HandleDamageText(hitTransform, outDamage, isCrit);

            if (enemyHandler == null) continue;
            if ( _dmgWhenShieldBreak.Activated && enemyHandler.EnemiesCore != null)
            {
                _dmgWhenShieldBreak.ApplyEffect(enemyHandler);
            }
            HandleSpawnPickup(hitTransform, VFXPickups.PickupType.Soul, enemyHandler);
        }
    }
    
    private void HandleSpawnPickup(Transform hitTransform, VFXPickups.PickupType pickupType = VFXPickups.PickupType.Gold, EnemyHandler enemyHandler = null)
    {
        // not enemy
        if (enemyHandler == null)
        {
            EarnHealth e = hitTransform.GetComponent<EarnHealth>();
            if (e != null)
            {
                if (!e.Dropped) return;
                if(OnSpawnPickup != null) OnSpawnPickup.Invoke(hitTransform, e.Type);
            }
        }
        // enemy
        else
        {
            if (!enemyHandler.EnemyStats.isDead) return;
            
            if(OnSpawnPickup != null) OnSpawnPickup.Invoke(hitTransform, pickupType);
        }
    }

    private bool IsEnemy(Transform hitTransform)
    {
        return !hitTransform.CompareTag("BreakableProps") && !hitTransform.CompareTag("TreasureChest");
    }

    private void HandleDamageText(Transform hitTransform, float outDamage, bool isCrit)
    {
        bool isEnemy = IsEnemy(hitTransform);

        if (!isEnemy) return;
        if(OnHitLanded != null) OnHitLanded.Invoke(hitTransform, outDamage, isCrit);
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
        playerMovement.canMove = true;
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
