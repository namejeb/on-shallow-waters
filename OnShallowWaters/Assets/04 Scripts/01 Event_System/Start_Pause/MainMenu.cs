using System;
using System.Collections;
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
            loadingScreen.gameObject.SetActive(true);
            GameManager.SetIsTutorial(false);
            StartCoroutine(LoadLevelScene());
        }

        public void ExitGame(){
            Application.Quit();
        }

        public void Tutorial()
        {
            loadingScreen.gameObject.SetActive(true);
            GameManager.SetIsTutorial(true);
            StartCoroutine(LoadLevelScene());
        }
        
        private IEnumerator LoadLevelScene()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadSceneAsync(1);
        }
    }
}
