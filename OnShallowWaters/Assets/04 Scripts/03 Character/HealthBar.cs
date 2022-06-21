using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform midBar;
    [SerializeField] private Transform frontBar;

    private Camera _cam;
    private Quaternion _camRot;
    
    private void OnDestroy()
    {
        LeanTween.reset();
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        _camRot = _cam.transform.rotation;
        transform.LookAt(transform.position + _camRot * Vector3.forward, _camRot * Vector3.up);
    }

    public void UpdateHealthBar(float percentage)
    {
        LeanTween.scaleX(frontBar.gameObject, percentage, .01f);
        LeanTween.scaleX(midBar.gameObject, percentage, .3f);
    }
}
