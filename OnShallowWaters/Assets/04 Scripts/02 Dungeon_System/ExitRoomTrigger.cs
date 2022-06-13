using UnityEngine;
using System;


public class ExitRoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomEntranceDir nextRoomEntranceDir;

    private bool isTriggered = false;

    public static event Action<RoomEntranceDir> OnExitRoom;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (isTriggered) return;

            if (OnExitRoom != null)
            {
                //isTriggered = true;
                OnExitRoom.Invoke(nextRoomEntranceDir);
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
