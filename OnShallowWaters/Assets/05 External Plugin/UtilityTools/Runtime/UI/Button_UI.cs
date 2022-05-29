using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class Button_UI : MonoBehaviour
{
    private Button _button;

    public delegate void ClickEventDelegate(UnityAction clickAction);
    public ClickEventDelegate ClickEvent;

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    
    private void Awake()
    {
        Button button = transform.GetComponent<Button>();
        if (button == null)
        {
            button = transform.gameObject.AddComponent<Button>();
        }

        _button = button;

        ClickEvent = Event_Click;
    }

    public void Event_Click(UnityAction clickAction)
    {
        _button.onClick.AddListener(clickAction);
    }
}
