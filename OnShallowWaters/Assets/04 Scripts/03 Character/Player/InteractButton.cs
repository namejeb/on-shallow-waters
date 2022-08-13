using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractButton : MonoBehaviour
{
    [Header("System: ")]
    [SerializeField] private Button button;
    public static event Action OnInteract;
    


    private void OnDestroy()
    {
        button.onClick.RemoveListener( DispatchEvent );
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener( DispatchEvent );
    }

    private void DispatchEvent()
    {
        if(OnInteract != null) OnInteract.Invoke();
    }
    
}
