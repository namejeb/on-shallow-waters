using UnityEngine;

public class DashNAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CharacterController controller;
    
    [SerializeField] private float dashDuration = 3f;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    
    private bool _isDash = false;
 
    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _elapsedTime;
    private float _endTime = 0f;


    private void Update()
    {
        if (_isDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        controller.Move((_endPos - _startPos) * speed * Time.deltaTime);

        if (Time.time > _endTime)
        {
            _isDash = false;
            playerMovement.enabled = true;
        }

        Debug.Log("dash");
    }

    public void ActivateDash()
    {
        _isDash = true;
        playerMovement.enabled = false;
        
        _startPos = transform.position;
        _endPos = (transform.forward + transform.position) * range;

        _endTime = Time.time + dashDuration;
    }
}
