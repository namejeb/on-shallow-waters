using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 moveDir;
    public float speed;

    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime; 
    }

    public void SetDirection(Transform myTarget)
    {
        moveDir = (myTarget.position - transform.position).normalized;

        // if got change in z then dont need recalculate
        moveDir = new Vector3 (moveDir.x, 0, moveDir.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerStats>().Damage(10);
            gameObject.SetActive(false);

        }
    }
}
