using UnityEngine;
using System;

public class ChangeRoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomEntranceDir nextRoomEntranceDir;

    private bool isTriggered = false;

    public static event Action<RoomEntranceDir> OnChangeRoom;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (isTriggered) return;

            if (OnChangeRoom != null)
            {
                //isTriggered = true;
                OnChangeRoom.Invoke(nextRoomEntranceDir);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // playerEntered = false;
        }
    }
}
