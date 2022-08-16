

public class EarnHealth : EarnPickups
{
    // Heals player based on specified minMax amounts
    
    private PlayerStats _playerStats;

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

}
