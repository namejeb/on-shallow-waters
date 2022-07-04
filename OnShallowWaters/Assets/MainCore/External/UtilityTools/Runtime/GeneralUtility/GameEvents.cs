using System;

public static class GameEvents
{
    public static event Action OnLoadEvent;

  
    public static void DispatchOnLoadEvent()
    {
        if (OnLoadEvent != null)
        {
            OnLoadEvent.Invoke();
        }
    }
}
