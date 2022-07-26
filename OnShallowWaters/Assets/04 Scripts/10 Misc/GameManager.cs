
public static class GameManager
{
    private static bool _isTutorial = false;
    public static bool IsTutorial { 
        get => _isTutorial;
    }


    public static void SetIsTutorial(bool status)
    {
        _isTutorial = status;
    }
}
