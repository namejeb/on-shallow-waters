using UnityEngine;

public class GoldShop : Shop
{
    [SerializeField] private int atkUpgradeAmt = 3;
    [SerializeField] private int defUpgradeAmt = 3;
    
    private PlayerStats _playerStats;
 
    
    private void Start()
    {
        _playerStats = PlayerStats.Instance;
    }
    
    public void UpgradeAtk()
    {
        Stat stat = _playerStats.Atk;
        int currValue = stat.BaseValue;
        int newValue = currValue + atkUpgradeAmt;
        
        _playerStats.Atk.ModifyBaseValue(newValue);
        
        //save to file
    }

    public void UpgradeDef()
    {
        Stat stat = _playerStats.Def;
        int currValue = stat.BaseValue;
        int newValue = currValue + defUpgradeAmt;
        
        _playerStats.Atk.ModifyBaseValue(newValue);
        
        //_playerStats.AddModifier(_playerStats.Def, 5);    -> soul shop upgrade method?
    }
}
