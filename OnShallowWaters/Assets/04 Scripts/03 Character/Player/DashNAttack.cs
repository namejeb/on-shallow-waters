using System;
using System.Collections;
using UnityEngine;
using _04_Scripts._05_Enemies_Bosses;
using UnityEngine.EventSystems;

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

    [SerializeField] private int outDamage;
    [SerializeField] private int inDamage;
    [SerializeField] private AttackButtonUI pressedButton;
    [SerializeField] private bool isSlashTigger;
    
    private BoonDamageModifiers _boonDamageModifiers;
    private SkBlessing _skBlessing;
    
    private void Awake()
    {
        _skBlessing = GetComponent<SkBlessing>();
    }

    private void Start()
    {
        stats = PlayerHandler.Instance.PlayerStats;
        _boonDamageModifiers = PlayerHandler.Instance.BoonDamageModifiers;
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
        animator.SetTrigger("Dash");
    }

    public void Update()
    {
        
        if(pressedButton.isPressed)
           chargedTimer += Time.deltaTime;

        //calcualtion for charge attacks here

        if (pressedButton.isPressed == false)
        {
            if (chargedTimer >= 1 && chargedTimer < 2)
            {

                isSlash = true;
                //Debug.Log("KAHHHHHHHBIIIIIN");
                if (isSlash)
                    HeavySlash();
            }

            else if (chargedTimer >= 2)
            {
                isSlam = true;
                //Debug.Log("BOMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                if (isSlam)
                    HeavySlam();
            }
            else if (chargedTimer >= 0.01)
            {
                //detect normal
                playerMovement.enabled = false;
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

        StartCoroutine(EnableMove(1f));
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

        StartCoroutine(EnableMove(1f));
    }


    public void Attack()
    {
        //Attack Sequence(What attack/aniamtion it will do)

        float baseAtk = (float) stats.Atk.CurrentValue;
        float atkPercent = (float) stats.AtkPercent;
        float tempOutDamage = 0f;

        

        if (attackSequence == 0 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (80f / 100f) * ((baseAtk + 0) * atkPercent);
         //   Debug.Log(tempOutDamage);
            animator.SetTrigger("Attack");
            attackSequence++;
            nextAttack = Time.time + 1;

            outDamage = Mathf.RoundToInt(tempOutDamage);
            HandleDamaging(tempOutDamage);

            StartCoroutine(EnableMove(0.5f));
        }
        else if (attackSequence == 1 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) ;
          //  Debug.Log(tempOutDamage);
            animator.SetTrigger("Attack2");
            attackSequence++;
            nextAttack = Time.time + 1;

            outDamage = Mathf.RoundToInt(tempOutDamage);
            HandleDamaging(tempOutDamage);

            StartCoroutine(EnableMove(0.5f));
        }
        else if (attackSequence == 2 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) ;
          //  Debug.Log(tempOutDamage);
            animator.SetTrigger("Attack3");
            attackSequence = 0;
            nextAttack = Time.time + 1.5f;

            outDamage = Mathf.RoundToInt(tempOutDamage);
            HandleDamaging(tempOutDamage);

            StartCoroutine(EnableMove(1.5f));
        }

        nextAttack /= stats.AtkSpeed;
        // Debug.Log(attackSequence.ToString());
    }

    private void HandleDamaging(float outDamage)
    {
        outDamage = Mathf.RoundToInt(outDamage);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f, damageableLayer);
        
        for (int i = 0; i < hitColliders.Length; i++)
        {

            Debug.Log(hitColliders[i].gameObject.name);
            if (hitColliders[i] == null) continue;  //skip if null
            
            IDamageable damagable = hitColliders[i].GetComponent<IDamageable>();
            if (damagable == null) continue;

            outDamage = ApplyCrit(outDamage);
            
            //if hit an enemy
            EnemyHandler enemyHandler = null;
            if (hitColliders[i].CompareTag("Enemy"))
            {
                enemyHandler = hitColliders[i].GetComponent<EnemyHandler>();
                if(enemyHandler != null)            //if still null, meaning its a boss
                    outDamage = (int) _boonDamageModifiers.ApplyModifiers(outDamage, enemyHandler); 
                
                _skBlessing.AddSoul(2);
            }
            damagable.Damage( (int) outDamage);
            if (enemyHandler == null) continue;
            if (_boonDamageModifiers.DmgWhenShieldBreakActivated && enemyHandler.EnemiesCore != null)
            {
                _boonDamageModifiers.ApplyShieldBreakDamage(enemyHandler);
            }
        }
    }

    private float ApplyCrit(float outgoingDamage)
    {
        float cr = UnityEngine.Random.Range(0f, 1f);
        if (cr < stats.CritChance)
        {
            outgoingDamage *= stats.CritDamage;
        }

        return outgoingDamage;
    }

    IEnumerator EnableMove(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        playerMovement.enabled = true;
    }


}
