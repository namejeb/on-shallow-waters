using UnityEngine;
using UnityEngine.SceneManagement;

namespace _04_Scripts._01_Event_System.Start_Pause {
    public class MainMenu : MonoBehaviour {
        public void StartGame(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ExitGame(){
            Application.Quit();
        }
    }
}
