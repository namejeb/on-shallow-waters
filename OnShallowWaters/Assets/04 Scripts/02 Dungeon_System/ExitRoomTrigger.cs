using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class ExitRoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomEntranceDir nextRoomEntranceDir;
    public static event Action<RoomEntranceDir> OnExitRoom;

    private GameObject _loadingScreen;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (RoomSpawnerV2.IsBossRoom)
                SceneManager.LoadScene( (int) SceneData.WinScene );
            else
                if (OnExitRoom != null) OnExitRoom.Invoke(nextRoomEntranceDir);
        }
    }
}
