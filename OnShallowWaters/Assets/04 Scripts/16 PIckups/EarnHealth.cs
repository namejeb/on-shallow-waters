
using UnityEngine;

public class EarnHealth : EarnPickups
{
    // Heals player based on specified minMax amounts
    [SerializeField] [Range(0f, 1f)] private float dropRate = .6f;
    private PlayerStats _playerStats;
    
    public bool Dropped => CheckDropRate();
    
    private void Start()
    {
        _playerStats = PlayerHandler.Instance.PlayerStats;
    }

    protected void HealPlayer(int amt)
    {
        _playerStats.Heal(amt);
    }

    protected void HealPlayer()
    {
        int amt = UnityEngine.Random.Range(minMaxAmount.x, minMaxAmount.y);
        _playerStats.Heal( amt );
    }


    public bool CheckDropRate()
    {
        float rate = UnityEngine.Random.Range(0f, 1f);
        if (rate < dropRate)
        {
            return true;
        }

        return false;
    }

}
