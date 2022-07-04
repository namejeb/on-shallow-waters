using UnityEngine;

public class StopCameraAtBounds : MonoBehaviour
{
    private Transform _cameraTarget;
    private SpriteRenderer _spriteRenderer;

    //Bounds
    [SerializeField] private float offsetBoundsX;
    [SerializeField] private float offsetBoundsZ;
    
    private float _upperBoundX;
    private float _lowerBoundX;
    
    private float _upperBoundZ;
    private float _lowerBoundZ;
    

    private void OnValidate()
    {
        offsetBoundsX = Mathf.Clamp(offsetBoundsX, 0, float.MaxValue);
        offsetBoundsZ = Mathf.Clamp(offsetBoundsZ, 0, float.MaxValue);
    }
    private void Start()
    {
        _cameraTarget = PlayerHandler.Instance.CameraTarget;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        InitBoundsValues();
    }

    private void Update()
    {
        #region IF_UNITY_EDITOR
            #if  UNITY_EDITOR
                InitBoundsValues();
            #endif
        #endregion
        
        KeepCameraInBounds();
    }

    private void KeepCameraInBounds()
    {
        Vector3 currPos = _cameraTarget.transform.position;
        Vector3 newPos = currPos;
        
        if (currPos.x >= _upperBoundX)
        {
            newPos.x = _upperBoundX - .1f;
        }
        else if (currPos.x <= _lowerBoundX)
        {
            newPos.x = _lowerBoundX + .1f;
        }
        
        if (currPos.z >= _upperBoundZ)
        {
            newPos.z = _upperBoundZ - .1f;
        }
        else if (currPos.z <= _lowerBoundZ)
        {
            newPos.z = _lowerBoundZ + .1f;
        }
        _cameraTarget.transform.position = newPos;
    }

    private void InitBoundsValues()
    {
        Bounds bounds = _spriteRenderer.bounds;
        _upperBoundX = bounds.max.x - offsetBoundsX; 
        _lowerBoundX = bounds.min.x + offsetBoundsX; 
        
        _upperBoundZ = bounds.max.z - offsetBoundsZ;
        _lowerBoundZ = bounds.min.z + offsetBoundsZ;
    }
}
