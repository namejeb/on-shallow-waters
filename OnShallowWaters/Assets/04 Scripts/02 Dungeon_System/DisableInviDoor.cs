using UnityEngine;

public class DisableInviDoor : MonoBehaviour
{
    private void OnDestroy()
    {
        BoonSelection.OnSelectedBoon -= OpenDoor;
    }

    private void Start()
    {
        BoonSelection.OnSelectedBoon += OpenDoor;
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
