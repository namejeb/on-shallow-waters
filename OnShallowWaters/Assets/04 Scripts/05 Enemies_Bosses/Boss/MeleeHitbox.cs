using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public int damage;
    public float delayBox, offBoxTime;
    public bool slam;

    BoxCollider bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();

        if (slam)
        {
            bc.enabled = false;
            StartCoroutine(EnableColldier());
        } 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // WARNING - Maybe in the future need modify the distance check if have any problem, cuz its hardcoded currently
            //if (Vector3.Distance(transform.position, col.transform.position) < 1)

            if (slam)
            {
                slam = false;
                col.gameObject.GetComponent<CrowdControl>().KnockUp();
            }

            Debug.Log("Player Damaged: " + damage);    
        }
    }

    IEnumerator EnableColldier()
    {
        yield return new WaitForSeconds(delayBox);
        bc.enabled = true;
        yield return new WaitForSeconds(offBoxTime);
        bc.enabled = false;
    }
}
