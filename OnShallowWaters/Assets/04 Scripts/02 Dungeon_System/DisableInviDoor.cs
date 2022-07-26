using UnityEngine;

public class DisableInviDoor : MonoBehaviour
{
    private void OnDestroy()
    {
        BoonSelection.OnSelectedBoon -= OpenDoor;
        Boss_Stats.OnBossDead -= OpenDoor;
    }

    private void OnEnable()
    {
        ResetDoor();
    }

    private void Start()
    {
        BoonSelection.OnSelectedBoon += OpenDoor;
        Boss_Stats.OnBossDead += OpenDoor;
    }

    private void OpenDoor()
    {
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void ResetDoor()
    {
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
