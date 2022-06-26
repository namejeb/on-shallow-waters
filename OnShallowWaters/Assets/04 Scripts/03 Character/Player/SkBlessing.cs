using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _04_Scripts._05_Enemies_Bosses;
using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;

public class SkBlessing : MonoBehaviour
{
    [SerializeField] private Image soulMeter;
    [SerializeField] private Button soulButton;

    [Header("SKB 1")]
    [SerializeField] private int atkAdd;
    [SerializeField] private int mvSpeedAdd;

    [Header("SKB 2")]
    [SerializeField] private int hpRegenAdd;
    [SerializeField] private int armRegenAdd;
    [SerializeField] private int regenAmount;
    [SerializeField] private float regenPerSec;

    [Header("SKB 4")]
    [SerializeField] private float executeDistance;
    [SerializeField] private float executePercentage;

    [Header("SKB 5")]
    [SerializeField] private int skb5Damage;

    private float timer, duration, requiredSoul, currSoul;
    private bool startCountdown;

    private PlayerStats playerStats;
    private TimeManager timeManager;

    public float Skb2Duration
    {
        get => regenPerSec * regenAmount;
    }

    public float Duration
    {
        get => duration;
        set => duration = value;
    }

    public float RequiredSoul
    {
        get => requiredSoul;
        set => requiredSoul = value;
    }

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void Start()
    {
        soulButton.interactable = false;

        // this is setting for skb, if 1st skb got change need change here too
        duration = 10;
        requiredSoul = 50;
        soulButton.onClick.AddListener(SKB1);
    }

    private void Update()
    {
        if (startCountdown)
        {
            timer -= Time.unscaledDeltaTime;
            soulMeter.fillAmount -= 1.0f / duration * Time.unscaledDeltaTime;
            if (timer < 0)
            {
                timer = duration;
                startCountdown = false;
                soulButton.interactable = false;
            }
        }
    }

    public void SKB1()
    {
        if (startCountdown)
            return;

        currSoul = 0;
        timer = duration;
        playerStats.Atk.AddModifier(atkAdd);
        playerStats.MovementSpeed.AddModifier(mvSpeedAdd);
        StartCoroutine(ResetCharacter(duration));
        startCountdown = true;
    }

    public void SKB2()
    {
        if (startCountdown)
            return;

        currSoul = 0;
        duration = regenPerSec * regenAmount;
        timer = duration;
        StartCoroutine(playerStats.RegenLoop(hpRegenAdd, armRegenAdd, regenAmount, regenPerSec));
        startCountdown = true;
    }

    public void SKB3()
    {
        if (startCountdown)
            return;

        currSoul = 0;
        timer = duration;
        startCountdown = true;
        timeManager.StartSlowMo(duration);
    }
    
    public void SKB4()
    {
        if (startCountdown)
            return;

        currSoul = 0;
        timer = duration;
        startCountdown = true;
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
        if (startCountdown)
            return;

        currSoul = 0;
        timer = duration;
        startCountdown = true;
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

    public void UpdateSoulMeter(float percentage)
    {
        soulMeter.fillAmount = percentage;
    }

    public void AddSoul(int soul)
    {
        if (startCountdown)
            return;

        if (!soulButton.interactable)
        {
            if (currSoul >= requiredSoul)
                currSoul = requiredSoul;
            else
                currSoul += soul;
            UpdateSoulMeter(currSoul / requiredSoul);
        }
        

        if (currSoul >= requiredSoul)
        {
            soulButton.interactable = true;
        }
    }
}
