using UnityEngine;

public class SpawnBoonTrigger : MonoBehaviour
{
    [SerializeField] private Transform boonTrigger;

    public void Spawn()
    {
        Instantiate(boonTrigger.gameObject, PlayerStats.Instance.transform.position + new Vector3(0f, 0f, 3f), Quaternion.identity);
    }
}
