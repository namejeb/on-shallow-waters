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
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
