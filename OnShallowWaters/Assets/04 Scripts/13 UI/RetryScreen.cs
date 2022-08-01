using UnityEngine;

public class RetryScreen : MonoBehaviour
{
    [SerializeField] private Transform retryScreenContainer;

    private void Awake()
    {
        retryScreenContainer.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (GameManager.IsRetry)
        {
            Invoke(nameof(Animate), 1f);
        }
    }

    private void Animate()
    {
        retryScreenContainer.gameObject.SetActive(true);
        GetComponent<Animator>().SetTrigger("fadeTrigger");
    }
}
