using UnityEngine;

public class WillDelete : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Time.deltaTime * speed, transform.eulerAngles.z);
    }
}
