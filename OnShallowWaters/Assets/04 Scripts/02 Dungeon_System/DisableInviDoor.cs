using UnityEngine;

public class DisableInviDoor : MonoBehaviour
{
    private void OnDestroy()
    {
        BoonSelection.OnSelectedBoon -= OpenDoor;
        Boss_Stats.OnBossDead -= OpenDoor;
        DummyStatsWithHp.OnDeath -= OpenDoor;
    }

    private void OnEnable()
    {
        ResetDoor();
    }

    private void Start()
    {
        BoonSelection.OnSelectedBoon += OpenDoor;
        Boss_Stats.OnBossDead += OpenDoor;
        DummyStatsWithHp.OnDeath += OpenDoor;
    }

    private void OpenDoor()
    {
        transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void ResetDoor()
    {
        transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
