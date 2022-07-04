using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public int currentHealth;

    private void Awake()
    {
        stats = PlayerHandler.Instance.PlayerStats;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReceiveDamage()
    {

    }
}
