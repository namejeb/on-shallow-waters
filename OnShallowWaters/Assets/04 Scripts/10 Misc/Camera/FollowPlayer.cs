using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform followPoint;

    private void Update()
    {
        transform.position = followPoint.position;
    }
}
