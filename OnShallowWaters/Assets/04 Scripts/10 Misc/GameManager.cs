
public static class GameManager
{
    public static bool IsTutorial { get; private set; }

    public static void SetIsTutorial(bool status)
    {
        IsTutorial = status;
    }
}
