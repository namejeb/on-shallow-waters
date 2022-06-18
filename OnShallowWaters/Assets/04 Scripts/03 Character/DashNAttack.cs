using UnityEngine;

public class DashNAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    
    [SerializeField] private float dashDuration = 3f;
    [SerializeField] private float range;
    [SerializeField] private float speed;

    [SerializeField] private LayerMask enemyLayer;
    
    
    private bool _isDash = false;
 
    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _elapsedTime;
    private float _endTime = 0f;


    private void Update()
    {
        if (_isDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        playerMovement.Move((_endPos - _startPos), speed);

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
        
        _startPos = transform.position;
        _endPos = (transform.forward + transform.position) * range;

        _endTime = Time.time + dashDuration;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        Collider[] enemies = Physics.OverlapSphere(transform.position, 5f, enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<EnemyHandler>().Damage(5);
            }
        }
    }
}
