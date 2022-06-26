using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControl : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private DashNAttack dna;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void KnockUp()
    {
        dna.enabled = false;
        pm.enabled = false;
        //rb.AddForce(transform.up * 10);
        rb.velocity = new Vector3(0, 10, 0);

        StartCoroutine(ReanbleScript());
    }

    IEnumerator ReanbleScript()
    {
        yield return new WaitForSeconds(time);
        pm.enabled = true;
        dna.enabled = true;
    }
}
