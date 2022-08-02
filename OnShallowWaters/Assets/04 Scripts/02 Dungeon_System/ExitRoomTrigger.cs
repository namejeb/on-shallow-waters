using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;


public class ExitRoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomEntranceDir nextRoomEntranceDir;
    public static event Action<RoomEntranceDir> OnExitRoom;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.IsTutorial)
            {
                StartCoroutine(LoadMainMenu());
                return;
            }
            
            if (RoomSpawnerV2.IsBossRoom)
                SceneManager.LoadScene( (int) SceneData.WinScene);
            else
                if (OnExitRoom != null) OnExitRoom.Invoke(nextRoomEntranceDir);
        }
    }

    private IEnumerator LoadMainMenu()
    {
        RoomSpawnerV2.TriggerTransitionStart();
        yield return new WaitForSeconds(RoomSpawnerV2.TransitionDuration);
        SceneManager.LoadScene( (int) SceneData.MainMenu );
    }
}
