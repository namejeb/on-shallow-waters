using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera _cam;
    private Quaternion _camRot;

    void Start()
    {
        _cam = Camera.main; 
    }
    

    void Update()
    {
        _camRot = _cam.transform.rotation;
        transform.LookAt(transform.position + _camRot * Vector3.forward, _camRot * Vector3.up);
    }
}
