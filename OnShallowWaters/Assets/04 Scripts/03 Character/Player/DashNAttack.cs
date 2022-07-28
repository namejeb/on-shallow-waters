using UnityEngine;
using _04_Scripts._05_Enemies_Bosses;
using System;
using System.Collections.Generic;
using UnityEngine.Timeline;
using System.Collections;

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

        Debug.Log("dash");
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
                //Debug.Log("KAHHHHHHHBIIIIIN");
                if (isSlash)
                    HeavySlash();
            }

            else if (chargedTimer >= 1)
            {
                isSlam = true;
                //Debug.Log("BOMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
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

        float baseAtk = (float) stats.Atk.CurrentValue;
        float atkPercent = (float) stats.AtkPercent;

        if (attackSequence == 0 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (80f / 100f) * ((baseAtk + 0) * atkPercent);
         //   Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack");
            attackSequence++;
            nextAttack = Time.time + 0.5f;
            StartCoroutine(EnableMove(0.5f));
            outDamage = Mathf.RoundToInt(tempOutDamage);
            //HandleDamaging(tempOutDamage);
        }
        else if (attackSequence == 1 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) ;
          //  Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack2");
            attackSequence++;
            nextAttack = Time.time + 0.8f;
            StartCoroutine(EnableMove(0.8f));
            outDamage = Mathf.RoundToInt(tempOutDamage);
            //HandleDamaging(tempOutDamage);
        }
        else if (attackSequence == 2 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) ;
          //  Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack3");
            attackSequence = 0;
            nextAttack = Time.time + 1f;
            StartCoroutine(EnableMove(1));
            outDamage = Mathf.RoundToInt(tempOutDamage);
            //HandleDamaging(tempOutDamage);
        }

        nextAttack /= stats.AtkSpeed;

        // Debug.Log(attackSequence.ToString());
    }

     public void HandleDamaging(float outDamage)
    {
        //outDamage = Mathf.RoundToInt(outDamage);

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
            
            
            // print(outDamage);
            //outDamage = ApplyCrit(outDamage);
            damagable.Damage( (int) outDamage);
           
            if (enemyHandler == null) continue;
            if ( _dmgWhenShieldBreak.Activated && enemyHandler.EnemiesCore != null)
            {
                //_boonDamageModifiers.ApplyShieldBreakDamage(enemyHandler);
                _dmgWhenShieldBreak.ApplyEffect(enemyHandler);
            }
        }
    }

    public float HandleBoonDmgModifications(float outDamage, EnemyHandler e)
    {
        // outDamage = (int) _boonDamageModifiers.ApplyModifiers(outDamage, enemyHandler); 
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

    IEnumerator EnableMove(float timer)
    {
        yield return new WaitForSeconds(timer);
        playerMovement.enabled = true;
    }

}
