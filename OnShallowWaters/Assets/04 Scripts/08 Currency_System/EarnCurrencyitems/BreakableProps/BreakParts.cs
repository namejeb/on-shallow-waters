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
        _partRbs = new Rigidbody[ _numOfParts ];

        // get references
        for (int i = 0; i < _numOfParts; i++)
        {
            _partRbs[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        RoomSpawnerV2.OnRoomChangeStart += DestroySelf;
        
        // break effect
        for (int i = 0; i < _numOfParts; i++)
        {
            _partRbs[i].AddForce( GetDirection() * flingForce, ForceMode.Impulse );
        }
        Invoke(nameof(HideParts), 3f);
    }


    private void SinkInSand()
    {
        for (int i = 0; i < _partRbs.Length; i++)
        {
            _partRbs[i].transform.GetComponent<Collider>().enabled = false;
            _partRbs[i].velocity = new Vector3(0f, -1f, 0f); 
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

    private void HideParts()
    {
        for (int i = 0; i < _numOfParts; i++)
        {
            _partRbs[i].transform.gameObject.SetActive(false);
        }
    }
}
