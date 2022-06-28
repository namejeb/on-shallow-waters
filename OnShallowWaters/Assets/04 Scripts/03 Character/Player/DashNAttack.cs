using System;
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
    [SerializeField] private float speed;

    [SerializeField] private LayerMask damageableLayer;

    [SerializeField] private int attackSequence = 0;
    [SerializeField] private float nextAttack;

    [SerializeField] private int outDamage;
    [SerializeField] private int inDamage;
    [SerializeField] private AttackButtonUI pressedButton;
    [SerializeField] private bool isSlashTigger;
    private BoonDamageModifiers _boonDamageModifiers;
    
    private bool _isDash = false;
 
    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _elapsedTime;
    private float _endTime = 0f;


    private void Awake()
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
    }

    public void Update()
    {
        if(pressedButton.isPressed)
           pressedButton.chargedTimer += Time.deltaTime;

        if (pressedButton.isSlash)
           HeavySlash();

        else if (pressedButton.isSlam)
            HeavySlam();
    }
    public void HeavySlash()
    {
        pressedButton.isSlash = false;
        float baseAtk = (float)stats.Atk.CurrentValue;
        float atkPercent = (float)stats.AtkPercent.CurrentValue;
        float tempOutDamage = 0f;
        tempOutDamage = (float)(130f / 100f) * ((baseAtk + 0) * atkPercent) * (100f / (100f + 50f));
        HandleDamaging(tempOutDamage);
        playerMovement.enabled = true;
        animator.SetTrigger("slashATK");
        attackSequence = 0;
    }

    public void HeavySlam()
    {
        pressedButton.isSlam = false;
        float baseAtk = (float)stats.Atk.CurrentValue;
        float atkPercent = (float)stats.AtkPercent.CurrentValue;
        float tempOutDamage = 0f;
        tempOutDamage = (float)(150f / 100f) * ((baseAtk + 0) * atkPercent) * (100f / (100f + 50f));
        HandleDamaging(tempOutDamage);
        playerMovement.enabled = true;
        animator.SetTrigger("slamATK");
        attackSequence = 0;
    }


    public void Attack()
    {
        //playerMovement.enabled = false;

        //Attack Sequence(What attack/aniamtion it will do)

        float baseAtk = (float) stats.Atk.CurrentValue;
        float atkPercent = (float) stats.AtkPercent.CurrentValue;
        float tempOutDamage = 0f;
        
        if (attackSequence == 0 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (80f / 100f) * ((baseAtk + 0) * atkPercent) * (100f/(100f + 50f));
         //   Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack");
            attackSequence++;
            nextAttack = Time.time + 1;
        }
        else if (attackSequence == 1 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) * (100f/(100f + 50f)) ;
          //  Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack2");
            attackSequence++;
            nextAttack = Time.time + 1;
        }
        else if (attackSequence == 2 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) * (100f/(100f + 50f)) ;
          //  Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack3");
            attackSequence = 0;
            nextAttack = Time.time + 1.5f;
        }
        outDamage = Mathf.RoundToInt(tempOutDamage);
        HandleDamaging(tempOutDamage);
        
     // Debug.Log(attackSequence.ToString());
    }

    private void HandleDamaging(float outDamage)
    {
        outDamage = Mathf.RoundToInt(outDamage);
        this.outDamage = (int) outDamage;
       
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f, damageableLayer);
        
        for (int i = 0; i < hitColliders.Length; i++)
        {   
            if (hitColliders[i] == null) continue;  //skip if null
            
     
            IDamageable damagable = hitColliders[i].GetComponent<IDamageable>();
            if (damagable == null) return;


            EnemyHandler enemyHandler = hitColliders[i].GetComponent<EnemyHandler>();
            if(enemyHandler != null)
            {
                outDamage = (int) _boonDamageModifiers.ApplyModifiers(outDamage, enemyHandler); 
            }

            damagable.Damage( (int) outDamage);
            print(outDamage);
            
            if (_boonDamageModifiers.DmgWhenShieldBreakActivated)
            {
                _boonDamageModifiers.ApplyShieldBreakDamage(enemyHandler);
            }
            
            // enemyHandler.EnemyStats.Damage( (int) outDamage);
            
            // if (hitColliders[i].CompareTag("TreasureChest"))
            // {
            //     hitColliders[i].GetComponent<TreasureChest>().Damage(0);
            //     continue;
            // }
            //
            // if (hitColliders[i].CompareTag("TrainingDummy"))
            // {
            //     hitColliders[i].GetComponent<TrainingDummy>().Damage(0);
            //     continue;
            // }
            //
            // if(hitColliders[i].CompareTag("BreakableProps"))
            // {
            //     hitColliders[i].GetComponent<BreakableProp>().Damage(0);
            //     continue;
            // }
        }
    }
}
