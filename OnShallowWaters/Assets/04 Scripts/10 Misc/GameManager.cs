using UnityEngine;



public class GameManager : MonoBehaviour
{
    private static bool _isTutorial = false;
    private static bool _isRetry = true;
    
    public static bool IsTutorial { get => _isTutorial; }

    public static bool IsRetry { get => _isRetry; }


    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public static void SetIsTutorial(bool status)
    {
        _isTutorial = status;
    }

    public static void SetIsRetry(bool status)
    {
        _isRetry = status;
    }
}
