using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _04_Scripts._05_Enemies_Bosses;
using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;

public class SkBlessing : MonoBehaviour
{
    [Header("SKB 1")]
    [SerializeField] private int atkAdd;
    [SerializeField] private int mvSpeedAdd;

    [Header("SKB 2")]
    [SerializeField] private int hpRegenAdd;
    [SerializeField] private int armRegenAdd;

    [Header("SKB 4")]
    [SerializeField] private float executeDistance;
    [SerializeField] private float executePercentage;

    [Header("SKB 5")]
    [SerializeField] private int skb5Damage;

    private float timer, duration;
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
        EnemiesCore[] enemies = FindObjectsOfType<EnemiesCore>();

        foreach (EnemiesCore enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;

            if (enemy.gameObject.activeInHierarchy && distanceToEnemy < executeDistance)
            {
                IDamageable e = enemy.GetComponent<IDamageable>();
                int damage = Mathf.CeilToInt(e.LostHP() * executePercentage);

                if (damage <= 0)
                    damage = 1;

                e.Damage(damage);
            }
        }
    }

    public void SKB5()
    {
        EnemiesCore[] enemies = FindObjectsOfType<EnemiesCore>();

        foreach (EnemiesCore enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.GetComponent<IDamageable>().Damage(skb5Damage);
            }
        }
    }
    
    IEnumerator ResetCharacter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("coroutine end");
        playerStats.Atk.RemoveModifier(atkAdd);
        playerStats.MovementSpeed.RemoveModifier(mvSpeedAdd);
    }

    
}
