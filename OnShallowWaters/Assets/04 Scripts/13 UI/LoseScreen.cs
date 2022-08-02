using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private Transform loseScreenContainer;
    [SerializeField] private float reloadDelay = 5f;
    

    private void OnDestroy()
    {
        PlayerStats.OnPlayerDeath -= EnableLoseScreen;
    }

    private void Awake()
    {
        loseScreenContainer.gameObject.SetActive(false);
    }

    private void Start()
    {
        PlayerStats.OnPlayerDeath += EnableLoseScreen;
    }

    private void EnableLoseScreen()
    {
        Invoke(nameof(Enable), 3f);
    }

    private void Enable()
    {
        loseScreenContainer.gameObject.SetActive(true);
        GetComponent<Animator>().SetTrigger("fadeTrigger");
        
        GameManager.SetIsRetry(true);
        Invoke(nameof(TriggerTransition), reloadDelay - 2f);
        Invoke(nameof(LoadLevelScene), reloadDelay);
    }

    private void TriggerTransition()
    {
        RoomSpawnerV2.TriggerTransitionStart();
    }
    private void LoadLevelScene()
    {
        SceneManager.LoadScene( (int) SceneData.LevelScene );
    }
    

}
