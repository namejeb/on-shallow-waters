using UnityEngine;
using System;

<<<<<<<< HEAD:OnShallowWaters/Assets/04 Scripts/07 Shop_System/ChangeRoomTrigger.cs
public class ChangeRoomTrigger : MonoBehaviour
========
public class ExitRoom : MonoBehaviour
>>>>>>>> development:OnShallowWaters/Assets/04 Scripts/07 Shop_System/ExitRoom.cs
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
