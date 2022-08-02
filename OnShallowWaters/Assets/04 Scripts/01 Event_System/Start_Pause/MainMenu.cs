using UnityEngine;
using UnityEngine.SceneManagement;


namespace _04_Scripts._01_Event_System.Start_Pause {
    public class MainMenu : MonoBehaviour {

        [SerializeField] private Transform loadingScreen;

        private void Awake()
        {
            loadingScreen.gameObject.SetActive(false);
        }

        public void StartGame(){
            PlayLoadingScreen();
            
            // Game settings
            GameManager.SetIsTutorial(false);
            GameManager.SetIsRetry(false);
            
            LoadLevelScene();
        }

        public void ExitGame(){
            Application.Quit();
        }

        public void Tutorial()
        {
            PlayLoadingScreen();
            
            // Game settings
            GameManager.SetIsTutorial(true);
            GameManager.SetIsRetry(false);
            
            LoadLevelScene();
        }
        
        private void LoadLevelScene()
        {
            SceneManager.LoadSceneAsync(1);
        }

        private void PlayLoadingScreen()
        {
           // loadingScreen.gameObject.SetActive(true);
            loadingScreen.Find("VideoPlayer").GetComponent<VideoPlaying>().VP.Play();
        }
    }
}
