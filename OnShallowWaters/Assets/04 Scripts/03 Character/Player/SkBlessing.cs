using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _04_Scripts._05_Enemies_Bosses;
using NaughtyAttributes;

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

        // this is default settings for skb, currently is skb3
        duration = 5;
        requiredSoul = 100;
        soulButton.onClick.AddListener(SKB3);
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

    private bool CanSpendSoul()
    {
        if (CurrencySystem.currencyDict[CurrencyType.SOULS] - 100 >= 0)
        {
            return true;
        }

        return false;
    }
    
    public void SKB1()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
        }
        else return;

        currSoul = 0;
        timer = duration;
        playerStats.Atk.AddModifier(atkAdd);
        playerStats.MovementSpeed.AddModifier(mvSpeedAdd);
        Debug.Log("Atk and Spd buff increase start");
        StartCoroutine(ResetCharacter(duration));
        startCountdown = true;
    }

    public void SKB2()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
        }
        else return;

        currSoul = 0;
        duration = regenPerSec * regenAmount;
        timer = duration;
        StartCoroutine(playerStats.RegenLoop(hpRegenAdd, armRegenAdd, regenAmount, regenPerSec));
        startCountdown = true;
    }

    [Button]
    public void SKB3()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
        }
        else return;

        currSoul = 0;
        timer = duration;
        startCountdown = true;

        timeManager.StartSlowMo(duration);
    }
    
    public void SKB4()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
        }
        else return;

        currSoul = 0;
        timer = duration;
        startCountdown = true;
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();
        Debug.Log("in");
        foreach (EnemyStats enemy in enemies)
        {
            Debug.Log("lol");
            float distanceToEnemy = (enemy.transform.position - transform.position).magnitude;
            Debug.Log("hai");
            if (enemy.gameObject.activeInHierarchy && distanceToEnemy < executeDistance)
            {
                IDamageable e = enemy.GetComponent<IDamageable>();
                int damage = Mathf.CeilToInt(e.LostHP() * executePercentage);

                if (damage <= 0)
                    damage = 1;
                Debug.Log(damage);
                e.Damage(damage);
            }
        }
    }

    public void SKB5()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
        }
        else return;

        currSoul = 0;
        timer = duration;
        startCountdown = true;
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

        foreach (EnemyStats enemy in enemies)
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
        Debug.Log("Atk & Spd buff end");
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
