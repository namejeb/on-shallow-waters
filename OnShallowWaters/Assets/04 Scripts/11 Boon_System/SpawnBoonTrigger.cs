using UnityEngine;

public class SpawnBoonTrigger : MonoBehaviour
{
    [SerializeField] private Transform boonTrigger;

    public static SpawnBoonTrigger Instance;

    private void Awake()
    {
        Instance = this;
    }

    //will remove
    public void Spawn()
    {
        PlayerStats playerStats = PlayerHandler.Instance.PlayerStats;
        Instantiate(boonTrigger.gameObject, playerStats.transform.position + new Vector3(0f, 3f, 3f), Quaternion.identity);
    }

    public void SpawnAtPosition(Vector3 position)
    {
        Instantiate(boonTrigger.gameObject, position + new Vector3(0f, 3f, 0f), Quaternion.identity);
    }
}
