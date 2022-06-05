using UnityEngine;
using NamejebTools.SaveSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    
    public void SavePlayer()
    {
        PlayerData playerData = new PlayerData(transform.position, transform.rotation);
        playerData.Save();
    }

    public void LoadPlayer()
    {
        PlayerData.Load();
        print(SaveDataMain.Current.PlayerData.PlayerLocation);
    }
}
