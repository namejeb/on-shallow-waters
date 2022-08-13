using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SkBlessing : MonoBehaviour
{
    [SerializeField] private Image soulMeter;
    [SerializeField] private Button soulButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button interactButton;


    [Header("SKB 1")]
    [SerializeField] private int atkAdd;
    [SerializeField] private int mvSpeedAdd;
    [SerializeField] private GameObject skb1_vfx;

    [Header("SKB 2")]
    [SerializeField] private int hpRegenAdd;
    [SerializeField] private int armRegenAdd;
    [SerializeField] private int regenAmount;
    [SerializeField] private float regenPerSec;
    [SerializeField] private GameObject skb2_vfx;

    [Header("SKB 4")]
    [SerializeField] private float executeDistance;
    [SerializeField] private float executePercentage;
    [SerializeField] private GameObject skb4_vfx;

    [Header("SKB 5")]
    [SerializeField] private int skb5Damage;

    private float timer, duration, requiredSoul, currSoul;
    private bool startCountdown;

    private PlayerStats playerStats;
    private TimeManager timeManager;
    private CinemachineImpulseSource impulse;
    private EnemyPooler pooler;
    public Button MainButton { get { return mainButton; } set { mainButton = value; } }
    public Button InteractButton { get { return interactButton; } set { interactButton = value; } }

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
        impulse = FindObjectOfType<CinemachineImpulseSource>();
        pooler = FindObjectOfType<EnemyPooler>();
    }

    private void Start()
    {
        soulButton.interactable = false;

        // this is default settings for skb, currently is skb1
        duration = 10;
        requiredSoul = 100;
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
            return;
        }

        skb1_vfx.SetActive(true);
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

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
            return;
        }

        skb2_vfx.SetActive(true);
        currSoul = 0;
        duration = regenPerSec * regenAmount;
        timer = duration;
        StartCoroutine(playerStats.RegenLoop(hpRegenAdd, armRegenAdd, regenAmount, regenPerSec, skb2_vfx));
        startCountdown = true;
    }

    public void SKB3()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
            return;
        }

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
            return;
        }

        skb4_vfx.SetActive(true);
        impulse.GenerateImpulse();
        currSoul = 0;
        timer = duration;
        startCountdown = true;
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

        foreach (EnemyStats enemy in enemies)
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
        StartCoroutine(DisableSKB_Obj(skb4_vfx));
    }

    public void SKB5()
    {
        if (startCountdown)
            return;

        if (CanSpendSoul())
        {
            CurrencySystem.RemoveCurrency(CurrencyType.SOULS, 100);
            return;
        }

        impulse.GenerateImpulse();
        currSoul = 0;
        timer = duration;
        startCountdown = true;
        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();

        if (enemies != null)
        {
            foreach (EnemyStats enemy in enemies)
            {
                if (enemy.gameObject.activeInHierarchy)
                {
                    Transform vfx = pooler.GetFromPool(ProjectileType.PlayerSKB5);
                    vfx.position = new Vector3(enemy.transform.position.x, 0.2f, enemy.transform.position.z);
                    vfx.gameObject.SetActive(true);
                    enemy.GetComponent<IDamageable>().Damage(skb5Damage);
                    StartCoroutine(DisableSKB_Obj(vfx.gameObject));
                }
            }
        }
        
    }

    IEnumerator DisableSKB_Obj(GameObject sfx)
    {
        yield return new WaitForSeconds(1);
        sfx.SetActive(false);
    }
    
    IEnumerator ResetCharacter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        skb1_vfx.SetActive(false);
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
        //soul = 100; // debug purpose
        if (!soulButton.interactable)
        {
            if (currSoul >= requiredSoul)
                currSoul = requiredSoul;
            else
                currSoul += soul;
            UpdateSoulMeter(currSoul / requiredSoul);
        }
        

        if (currSoul >= requiredSoul && CanSpendSoul())
        {
            soulButton.interactable = true;
            //soulButtonImage.color = Color.HSVToRGB(230, 100, 100, true);
        }
    }
}
