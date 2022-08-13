using UnityEngine;
using UnityEngine.VFX;

public class Torch : MonoBehaviour
{
    [SerializeField] private VisualEffect fireVFX;

    private void Awake()
    {
        SetFireActive(false);
    }

    private void OnDestroy()
    {
        BoonTrigger.OnPickedUp -= EnableFire;
    }
    
    private void OnEnable()
    {
        SetFireActive(false);
    }

    private void Start()
    {
        BoonTrigger.OnPickedUp += EnableFire;
    }

    private void SetFireActive(bool status)
    {
        if(isActiveAndEnabled)
            fireVFX.enabled = status;
    }

    private void EnableFire()
    {
        SetFireActive(true);
    }
}
