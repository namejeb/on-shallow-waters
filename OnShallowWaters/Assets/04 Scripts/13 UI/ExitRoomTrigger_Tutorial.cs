using System.Collections;
using _04_Scripts._01_Event_System.Start_Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoomTrigger_Tutorial : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Transform loadingScreenTransform;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.IsTutorial)
            {
                StartGame();
                mainMenu.Play();
            }
        }
    }
  
    
    private void StartGame()
    {
        loadingScreenTransform.gameObject.SetActive(true);
        StartCoroutine(LoadSceneWithDelay( SceneData.LevelScene ));
    }
    
            
    private IEnumerator LoadSceneWithDelay(SceneData sceneData)
    {
        yield return new WaitForSeconds( MainMenu.LoadingBuffer );
        LoadScene(sceneData);
    }
    
    private void LoadScene(SceneData sceneData)
    {
        SceneManager.LoadSceneAsync( (int) sceneData);
    }
}
