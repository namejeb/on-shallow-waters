using UnityEngine;

public class DisableInviDoor : MonoBehaviour
{
    private void OnDestroy()
    {
        BoonSelection.OnSelectedBoon -= OpenDoor;
        Boss_Stats.OnBossDead -= OpenDoor;
    }

    private void Start()
    {
        BoonSelection.OnSelectedBoon += OpenDoor;
        Boss_Stats.OnBossDead += OpenDoor;
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
