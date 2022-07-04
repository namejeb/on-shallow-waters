using UnityEngine;

public class MoveToFollowPoint : MonoBehaviour
{
    [SerializeField] private Transform followPoint;

    private void Update()
    {
        transform.position = followPoint.position;
    }
}
