using UnityEngine;
using _04_Scripts._05_Enemies_Bosses;

public class DashNAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private EnemyStats eStats;
    
    [SerializeField] private float dashDuration = 3f;
    [SerializeField] private float range;
    [SerializeField] private float speed;

    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private int attackSequence = 0;
    [SerializeField] private float nextAttack;

    [SerializeField] private int outDamage;
    [SerializeField] private int inDamage;
    
    
    private bool _isDash = false;
 
    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _elapsedTime;
    private float _endTime = 0f;


    private void Awake()
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
        playerMovement.Move(transform.forward, speed);

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
            Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack");
            attackSequence++;
            nextAttack = Time.time + 1;
        }
        else if (attackSequence == 1 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (90f / 100f) * ((baseAtk + 0) * atkPercent) * (100f/(100f + 50f)) ;
            Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack2");
            attackSequence++;
            nextAttack = Time.time + 1;
        }
        else if (attackSequence == 2 && Time.time > nextAttack)
        {
            tempOutDamage = (float) (100f / 100f) * ((baseAtk + 0) * atkPercent) * (100f/(100f + 50f)) ;
            Debug.Log(tempOutDamage);
            playerMovement.enabled = true;
            animator.SetTrigger("Attack3");
            attackSequence = 0;
            nextAttack = Time.time + 1.5f;
        }

        outDamage = Mathf.RoundToInt(tempOutDamage);
        

        //temp damage to test WaveSpawner, will remove
        Collider[] enemies = Physics.OverlapSphere(transform.position, 5f, enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
              //  EnemyHandler enemyHandler = enemies[i].GetComponent<EnemyHandler>();
                IDamageable damagable = enemies[i].GetComponent<IDamageable>();

                // if (enemyHandler != null)
                //     enemyHandler.Damage(5);

                if (damagable != null)
                    damagable.Damage(outDamage);

            }
        }
    }


    
}
