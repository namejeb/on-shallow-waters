using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(transform.position, col.transform.position) < 1)
                Debug.Log("Player Damaged" + damage);
        }
    }
}
