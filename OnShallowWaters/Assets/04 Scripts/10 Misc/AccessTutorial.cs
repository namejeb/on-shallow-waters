using UnityEngine;
using UnityEngine.UI;
using _04_Scripts._01_Event_System.Start_Pause;

public class AccessTutorial : MonoBehaviour
{
    [Header("System: ")] 
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private SkBlessing skBlessing;
    
    // Enable or Disable PlayerMovement
    [SerializeField] private PlayerMovement playerMovement;
    
 
    [Space][Space]
    [Header("UI: ")] 
    [SerializeField] private Transform accessTutorialNotice;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    private Button _attackButton;
    private Button _interactButton;
    
    private Outline _outline;

    private bool _canShow = false;
    

    private void OnDestroy()
    {
        InteractButton.OnInteract -= Show;
        
        yesButton.onClick.RemoveListener( EnterTutorial );
        noButton.onClick.RemoveListener( Hide );
    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    private void Start()
    {
        _attackButton = skBlessing.MainButton;
        _interactButton = skBlessing.InteractButton;
        InteractButton.OnInteract += Show;
        
        yesButton.onClick.AddListener( EnterTutorial );
        noButton.onClick.AddListener( Hide );
        Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _outline.enabled = true;
            SetInteractButtonActive( true );
            _canShow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _outline.enabled = false;
            SetInteractButtonActive( false );
            _canShow = false;
        }
    }

    private void SetInteractButtonActive(bool status)
    {
        if (status)
        {
            _attackButton.gameObject.SetActive(false);
            _interactButton.gameObject.SetActive(true);
        }
        else
        {
            
            _attackButton.gameObject.SetActive(true);
            _interactButton.gameObject.SetActive(false);
        }
    }

    private void Show()
    {
        if(_canShow)
            accessTutorialNotice.gameObject.SetActive( true );
    }

    private void Hide()
    {
        accessTutorialNotice.gameObject.SetActive( false );
        playerMovement.enabled = true;
    }
    
    private void EnterTutorial()
    {
        Hide();
        mainMenu.Tutorial();
    }
}
