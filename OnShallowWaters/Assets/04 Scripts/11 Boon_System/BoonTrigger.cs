using UnityEngine;

public class BoonTrigger : MonoBehaviour
{
    //spawn when last enemy dies

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoonSelection.Instance.RollBoons();
            Destroy(gameObject);
        }
    }
}
