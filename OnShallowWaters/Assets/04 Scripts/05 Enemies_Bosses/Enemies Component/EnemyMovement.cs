using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private bool chaseMode, walkMode, awayMode;

    private Rigidbody _rb;
    private Enemies_Stats _es;

    private bool _isAttacking;
    private Vector3 movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _es = GetComponent<Enemies_Stats>();
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
            return;
        
        if (chaseMode)
        {
            //movement 1
            //keep chase player [maybe use navmesh]
        }
        else if (walkMode)
        {
            //movement 2
            //walk a while, stop, attack, repeat again
            
            RandomDirection(movement);
            _rb.MovePosition(_rb.position + movement.normalized * _es.Speed.CurrentValue * Time.fixedDeltaTime);
        }
        else if (awayMode)
        {
            //movement 3
            //go away from player
            // if (Vector3.dis)
        }

    }

    private Vector3 RandomDirection(Vector3 m)
    {
        return m = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
    }
}
