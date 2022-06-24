using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Rigidbody rb;
    public Joystick joystick;

    public float speed = 6f;
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;
    float stunTime;

    private Vector3 _moveDir;
    private void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        _moveDir = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (_moveDir.magnitude >= 0.2f)
        {
            float targetAngle = Mathf.Atan2(_moveDir.x, _moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void FixedUpdate()
    {      
         rb.velocity = Vector3.zero;
         Move(_moveDir, speed);
    }

    public void Move(Vector3 direction, float speed)
    {
        rb.velocity = new Vector3(direction.x, 0f, direction.z) * speed;
    }

    

    
}
