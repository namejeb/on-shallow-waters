using UnityEngine;
using UnityEngine.UI;


public enum InputMode
{
    BUTTONS,
    BUTTONLESS
}


public class InputManager : MonoBehaviour
{
    [SerializeField] private Button[] inputButtons;
    [SerializeField] private SwipeDash swipeDash;
    
    private void Start()
    {
        InitInputMode(GameSettings.InputMode);
    }

    private void InitInputMode(InputMode mode)
    {
        if (mode == InputMode.BUTTONS)
        {
            for (int i = 0; i < inputButtons.Length; i++)
            {
                inputButtons[i].gameObject.SetActive(true);
            }

            swipeDash.enabled = false;
        }
        else
        {
            for (int i = 0; i < inputButtons.Length; i++)
            {
                inputButtons[i].gameObject.SetActive(false);
            }
            swipeDash.enabled = true;
        }
    }
    
}
