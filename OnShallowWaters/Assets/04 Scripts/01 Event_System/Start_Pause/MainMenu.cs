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

            if (GameManager.IsFirstPlayThrough)
            {
                StartCoroutine(LoadSceneWithDelay(SceneData.CutsceneScene));
                return;
            }

            // Game settings
            GameManager.SetIsTutorial(false);
            GameManager.SetIsRetry(false);
            
            StartCoroutine(LoadSceneWithDelay( SceneData.LevelScene ));
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

            StartCoroutine(LoadSceneWithDelay( SceneData.LevelScene ));
        }
        
        private void LoadScene(SceneData sceneData)
        {
            SceneManager.LoadSceneAsync( (int) sceneData);
        }
        
        private IEnumerator LoadSceneWithDelay(SceneData sceneData)
        {
            yield return new WaitForSeconds(1f);
            LoadScene(sceneData);
        }

        private void PlayLoadingScreen()
        {
            loadingScreen.gameObject.SetActive(true);
        }
    }
}
