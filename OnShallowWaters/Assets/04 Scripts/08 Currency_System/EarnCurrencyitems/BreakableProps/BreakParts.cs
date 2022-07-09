using UnityEngine;
using Random = UnityEngine.Random;

public class BreakParts : MonoBehaviour
{
    [SerializeField] private float flingForce = 5f;
    
    private Rigidbody[] _partRbs;
    private int _numOfParts;

    private void OnDisable()
    {
        RoomSpawnerV2.OnRoomChangeStart -= DestroySelf;
    }

    private void Awake()
    {
        _numOfParts = transform.childCount;
        _partRbs = new Rigidbody[_numOfParts];

        for (int i = 0; i < _numOfParts; i++)
        {
            _partRbs[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        RoomSpawnerV2.OnRoomChangeStart += DestroySelf;
        
        for (int i = 0; i < _numOfParts; i++)
        {
            _partRbs[i].AddForce( GetDirection() * flingForce, ForceMode.Impulse );
        }
    }

    private Vector3 GetDirection()
    {
        float xDir = Random.Range(-1f, 1f);
        float yDir = Random.Range(-1f, 1f);
        float zDir = Random.Range(-1f, 1f);

        Vector3 flingDir = new Vector3(xDir, yDir, zDir);
        return flingDir;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
