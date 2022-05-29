using NamejebTools.SaveSystem;
using System.Collections.Generic;



[System.Serializable]
public class SaveDataMain 
{
    #region Singleton

    private static SaveDataMain _current;


    public static SaveDataMain Current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveDataMain();
            }
            
            return _current;
        }

        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }
    #endregion
    
   
    public PlayerData PlayerData { get; set; }

    
    public List<EnemyData> EnemyData { get; set; }

}
