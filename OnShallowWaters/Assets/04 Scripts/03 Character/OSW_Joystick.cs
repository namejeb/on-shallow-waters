using UnityEngine;
using UnityEngine.EventSystems;


public class OSW_Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float sensitivity;
    [SerializeField] private float deadZone;
    
    private Canvas _canvas;
    private RectTransform _baseRect;

    private RectTransform background;
    private RectTransform handle;
  
    private float _magnitude;
    public static Vector3 MovementVector { get; private set; }

    private void Start()
    {
        _baseRect = GetComponent<RectTransform>();
        _canvas = transform.parent.GetComponent<Canvas>();

        background = transform.GetChild(0).GetComponent<RectTransform>();
        handle = background.transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, eventData.position, eventData.pressEventCamera, out direction))
        {
            direction = direction.normalized;

            _magnitude = direction.sqrMagnitude * sensitivity;

            if (_magnitude >= deadZone)
            {
                MovementVector = new Vector3(direction.x, 0f, direction.y) * _magnitude;
            }
            else
            {
                MovementVector = Vector3.zero;
            }
            HandleJoystickMovement(eventData);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        handle.gameObject.SetActive(false);

        MovementVector = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive((true));
        handle.gameObject.SetActive(true);
        
        handle.anchoredPosition = Vector2.zero;
    
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            background.anchoredPosition = localPoint;
        }
    }
    
    private void HandleJoystickMovement(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, background.position);
        Vector2 radius = background.sizeDelta * .5f;
        Vector2 input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            
        handle.anchoredPosition = input * radius * sensitivity;
        handle.anchoredPosition = Vector2.ClampMagnitude(handle.anchoredPosition, 50f);
    }
}
