using UnityEngine;
using UnityEngine.SceneManagement;

namespace _04_Scripts._01_Event_System.Start_Pause {
    public class MainMenu : MonoBehaviour {
        public void StartGame(){
            LoadLevelScene();
        }

        public void ExitGame(){
            Application.Quit();
        }

        public void Tutorial()
        {
            LoadTutorialScene();
        }


        private void LoadLevelScene()
        {
            SceneManager.LoadScene(1);
        }

        private void LoadTutorialScene()
        {
            SceneManager.LoadScene(3);
        }
    }
}
