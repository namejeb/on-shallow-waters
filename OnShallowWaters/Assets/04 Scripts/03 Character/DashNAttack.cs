using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DashNAttack : MonoBehaviour
{
    PlayerMovement moveScript;

    [SerializeField] private Button dashButton;

    private Vector3 startPos;
    private Vector3 endPos;
    private float elapsedTime;
    [SerializeField] private float dashDuration = 3f;
    private bool isDash;

    void Start()
    {
        isDash = false;
    }
    void Update()
    {
        if (isDash)
        {
            Dash();
            
        }
    }

    public void Dash()
    {
        isDash = true;
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / dashDuration;
        //Vector3 dash = new Vector3(transform.position.x * dashSpeed, 0f, transform.position.z * dashSpeed);
        transform.position = Vector3.Lerp(startPos, endPos, percentComplete);
        Debug.Log("dash");

        if (percentComplete > 1.0f)
        {
            isDash = false;
        }
        

    }
}
