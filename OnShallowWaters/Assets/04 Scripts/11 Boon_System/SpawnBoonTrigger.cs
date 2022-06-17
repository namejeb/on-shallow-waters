using UnityEngine;

public class SpawnBoonTrigger : MonoBehaviour
{
    [SerializeField] private Transform boonTrigger;

    public void Spawn()
    {
        PlayerStats playerStats = PlayerHandler.Instance.PlayerStats;
        Instantiate(boonTrigger.gameObject, playerStats.transform.position + new Vector3(0f, 0f, 3f), Quaternion.identity);
    }
}
