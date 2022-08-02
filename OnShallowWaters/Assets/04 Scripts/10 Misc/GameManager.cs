using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static bool _isTutorial = true;
    private static bool _isRetry = false;
    private static bool _isFirstPlayThrough = true;
    
    
    public static bool IsTutorial { get => _isTutorial; }
    public static bool IsRetry { get => _isRetry; }
    public static bool IsFirstPlayThrough { get => _isFirstPlayThrough; }


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

    public static void SetIsFirstPlayThrough(bool status)
    {
        _isFirstPlayThrough = status;
    }
}
