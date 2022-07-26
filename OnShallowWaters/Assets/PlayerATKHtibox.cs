using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATKHtibox : MonoBehaviour
{
    BoxCollider hitbox;
    private void Awake()
    {
        hitbox.GetComponent<BoxCollider>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(gameObject.name);
        }
    }
}
