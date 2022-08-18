using _04_Scripts._01_Event_System.Start_Pause;
using UnityEngine;
using System;

public class ExitRoomTrigger_Tutorial : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    public static event Action OnExitTutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.IsTutorial)
            {
                if(OnExitTutorial != null) OnExitTutorial.Invoke();
                RoomSpawnerV2.TriggerTransitionStart();
                mainMenu.Play();
            }
        }
    }
}
