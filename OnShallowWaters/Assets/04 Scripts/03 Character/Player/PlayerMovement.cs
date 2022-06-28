using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Rigidbody rb;
    private PlayerStats _playerStats;
    public Joystick joystick;

    public float speed = 6f;
    public float rotationSpeed = 10f;
    public bool canMove = true;

    private Vector3 _moveDir;

    private void Awake()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
        canMove = true;
    }
    private void Update()
    {
        if (canMove)
        {
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;
            _moveDir = new Vector3(horizontal, 0f, vertical).normalized;
        }
        else
            _moveDir = Vector3.zero;

        if (_moveDir.magnitude >= 0.2f)
        {
            //float targetAngle = Mathf.Atan2(_moveDir.x, _moveDir.z) * Mathf.Rad2Deg;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
            RotateTowards();
        }
    }

    private void RotateTowards()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_moveDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.unscaledDeltaTime * rotationSpeed);
    }

    private void FixedUpdate()
    {      
         rb.velocity = Vector3.zero;
         Move(_moveDir, speed, false);
    }

    public void Move(Vector3 direction, float speed, bool isDash)
    {
        float dashModifier = 1f;
        if (isDash) dashModifier = speed;
        rb.velocity = new Vector3(direction.x, 0f, direction.z) * (dashModifier * _playerStats.MovementSpeed.CurrentValue * _playerStats.MovementSpeedMultiplier ) / Time.timeScale;
    }
}
