using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody rb;

    public float speed = 6f;
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;


    private void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (direction.magnitude >= 0.2f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Move(direction, speed);
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        //transform.position = Vector3.MoveTowards(transform.position, direction * 100, speed * Time.deltaTime);
        rb.MovePosition(Vector3.MoveTowards(transform.position, direction * 100, speed * Time.deltaTime));
    }
}
