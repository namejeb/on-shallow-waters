using UnityEngine;

public class CameraTargetFollowPoint : MonoBehaviour
{
    [SerializeField] private Transform playerModel;
    [SerializeField] private float zOffset;
    private float _startY;

    private void Start()
    {
        _startY = transform.position.y;
    }
    void Update()
    {
        Vector3 forwardVector = playerModel.forward;
        Vector3 newPos =  playerModel.position + (forwardVector * zOffset) ;
        transform.position = newPos + new Vector3(0f, _startY, 0f);
    }
}
