using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> walls = new List<GameObject>();
    public bool doorTriggerd;

    public void SetWallStatus(bool status)
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(status);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!doorTriggerd)
            {
                SetWallStatus(true);
            }
            doorTriggerd = true;
        }
    }
}
