
public static class GameSettings
{
    private static InputMode _mode = InputMode.BUTTONLESS;

    public static InputMode InputMode
    {
        get => _mode; 
        set => _mode = value;
    }
}
