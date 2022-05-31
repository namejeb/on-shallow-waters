using UnityEditor;
using UnityEngine;

[ExecuteInEditMode] 
public class TextureTiling : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float tileSize = 1f;
    
    private MeshRenderer _meshRenderer;
    
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        UpdateTiling();
    }

    #if UNITY_EDITOR 
    private void Update()
    {
        if (!EditorApplication.isPlaying)   //treat editor playing as actual built game
        {
            if (transform.hasChanged)
            {
                UpdateTiling();
                transform.hasChanged = false;
            }
        }
    }
    #endif

    private void UpdateTiling()
    {
        Material tempMat = new Material(material);

        tempMat.mainTextureScale = transform.localScale * tileSize;
        _meshRenderer.material = tempMat;
    }
}
