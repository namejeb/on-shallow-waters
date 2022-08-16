using UnityEngine;
using UnityEngine.SceneManagement;

namespace _04_Scripts._01_Event_System.Start_Pause {
    public class PauseButton : MonoBehaviour{
        public GameObject pauseMenu;
        public bool isPaused;
        public SoundData UISFX;
        private void Start(){
            pauseMenu.SetActive(false);
            isPaused = false;
        }

        public void PauseGame(){
            if (isPaused) return;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            SoundManager.instance.PlaySFX(UISFX, "UISFX");
         
        }
        
        public void ResumeGame(){
            if (!isPaused) return;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;

        }

        public void ReturnMainMenu(){
            Time.timeScale = 1f;
            isPaused = false;
            SceneManager.LoadScene("MainMenu");
        }

        public void ExitApplication(){
            Application.Quit();
        }
    }
}
