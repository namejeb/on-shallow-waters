using UnityEngine;

public class BoonTrigger : MonoBehaviour
{
    //spawn when last enemy dies
  
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            BoonSelection.Instance.RollBoons();
            Destroy(gameObject);
        }
    }
}
