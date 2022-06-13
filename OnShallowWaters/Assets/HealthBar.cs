using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform midBar;
    [SerializeField] private Transform frontBar;

    private void OnDestroy()
    {
        LeanTween.reset();
    }

    private void Start()
    {
        Camera cam = Camera.main;
        Quaternion camRot = cam.transform.rotation;
        
        transform.LookAt(transform.position + camRot * Vector3.forward, camRot * Vector3.up);
        
       // UpdateHealthBar(.5f);
    }

    private void UpdateHealthBar(float percentage)
    {
        LeanTween.scaleX(frontBar.gameObject, percentage, .01f);
        LeanTween.scaleX(midBar.gameObject, percentage, .3f);
    }
}
