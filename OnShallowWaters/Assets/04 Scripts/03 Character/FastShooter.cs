using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShooter : Enemies_Core{
    private Color tintColor = Color.green;
    public GameObject projectile;
    public bool fireOnce;

    void Awake(){
        behaviour = core_stage.Move;
    }

    protected override void Update(){
        base.Update();
        RaycastSingle();
    }

    private void RaycastSingle(){
        Vector3 origin = transform.position;
        Vector3 dirct = transform.forward;
        float maxDist = 5f;

        Debug.DrawRay(origin, dirct * maxDist, Color.red);
        Ray ray = new Ray(origin, dirct);

        bool result = Physics.Raycast(ray, out RaycastHit hitInfo, maxDist);

        if(result && hitInfo.collider.CompareTag("Player")){
            hitInfo.collider.GetComponent<Renderer>().material.color = tintColor;
            if(fireOnce) return;
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            fireOnce = true;
        }
    }
}
