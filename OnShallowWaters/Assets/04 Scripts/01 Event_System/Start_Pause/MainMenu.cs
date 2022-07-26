using UnityEngine;
using UnityEngine.SceneManagement;

namespace _04_Scripts._01_Event_System.Start_Pause {
    public class MainMenu : MonoBehaviour {
        public void StartGame(){
            GameManager.SetIsTutorial(false);
            LoadLevelScene();
        }

        public void ExitGame(){
            Application.Quit();
        }

        public void Tutorial()
        {
            GameManager.SetIsTutorial(true);
            LoadLevelScene();
        }
        
        private void LoadLevelScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
