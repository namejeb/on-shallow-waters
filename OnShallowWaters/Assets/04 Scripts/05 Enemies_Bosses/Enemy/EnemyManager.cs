using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ISaveData
{
    private List<EnemyData> _enemyDataList;

    private const string SaveName = "EnemyDataList";
    
    public void SaveEnemyDataList()
    {
        ISaveData.SaveData(SaveName, _enemyDataList);
    }

    public void LoadEnemyDataList()
    {
        SaveDataMain.Current.EnemyData = (List<EnemyData>) ISaveData.LoadData(SaveName);
    }
}
