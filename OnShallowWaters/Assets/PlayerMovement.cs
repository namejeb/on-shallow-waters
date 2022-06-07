using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    //public Button dashButton;
    public Joystick joystick;

    public float speed = 6f;
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        //Button button = dashButton.GetComponent<Button>();
    }
    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (direction.magnitude >= 0.2f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        } 


        

    }

    public void Dash()
    {

    }

}
