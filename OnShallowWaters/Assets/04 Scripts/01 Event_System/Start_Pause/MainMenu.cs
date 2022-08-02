using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace _04_Scripts._01_Event_System.Start_Pause {
    public class MainMenu : MonoBehaviour {

        [SerializeField] private Transform loadingScreen;

        private void Start()
        {
            loadingScreen.gameObject.SetActive(false);
        }

        public void StartGame(){
            PlayLoadingScreen();
            
            // Game settings
            GameManager.SetIsTutorial(false);
            GameManager.SetIsRetry(false);
            
            // LoadLevelScene();
            StartCoroutine(LoadLevelSceneWithDelay());
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
            
            //LoadLevelScene();
            StartCoroutine(LoadLevelSceneWithDelay());
        }
        
        private void LoadLevelScene()
        {
            SceneManager.LoadSceneAsync(1);
        }
        
        private IEnumerator LoadLevelSceneWithDelay()
        {
            yield return new WaitForSeconds(1f);
            LoadLevelScene();
        }

        private void PlayLoadingScreen()
        {
            loadingScreen.gameObject.SetActive(true);
        }
    }
}
