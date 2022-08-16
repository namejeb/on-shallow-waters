using _04_Scripts._01_Event_System.Start_Pause;
using UnityEngine;

public class ExitRoomTrigger_Tutorial : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.IsTutorial)
            {
                mainMenu.Play();
            }
        }
    }
}
