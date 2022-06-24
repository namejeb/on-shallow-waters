using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkBlessing : MonoBehaviour
{
    [SerializeField] private int atkAdd, mvSpeedAdd, hpRegenAdd, armRegenAdd;
    [SerializeField] private float duration;
    private float timer;
    private bool startCountdown;

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (startCountdown)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = duration;
                startCountdown = false;
            }
        }
    }

    public void SKB1()
    {
        playerStats.Atk.AddModifier(atkAdd);
        playerStats.MovementSpeed.AddModifier(mvSpeedAdd);
        Debug.Log(playerStats.Atk.CurrentValue);
        timer = duration;
        StartCoroutine(ResetCharacter(duration));
        startCountdown = true;
    }

    public void SKB2()
    {
        StartCoroutine(playerStats.RegenLoop(hpRegenAdd, armRegenAdd, 5));
    }

    public void SKB3()
    {
        Debug.Log("slow down time");
    }

    public void SKB4()
    {
        Debug.Log("execute low health");
    }

    public void SKB5()
    {
        Debug.Log("skb5");
    }
    
    IEnumerator ResetCharacter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("coroutine end");
        playerStats.Atk.RemoveModifier(atkAdd);
        playerStats.MovementSpeed.RemoveModifier(mvSpeedAdd);
    }

    
}
