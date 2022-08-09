using UnityEngine;
using System;

public class BoonTrigger : MonoBehaviour
{
    //spawn when last enemy dies
    public static event Action OnPickedUp;
  
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            BoonSelection.Instance.RollBoons();
            if(OnPickedUp != null) OnPickedUp.Invoke();
            Destroy(gameObject);
        }
    }
}
