using System;
using UnityEngine;

public class TestWave : MonoBehaviour
{
    public static Wave CurrWave;
    public static int RoomTotalEnemies;
    public bool IsLastEnemy => RoomTotalEnemies == 0;

    public static TestWave Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        WaveSpawner.OnChangeWave -= UpdateWave;
        WaveSpawner.OnChangeRoom -= UpdateRoomTotalEnmemies;
    }

    private void Start()
    {
        WaveSpawner.OnChangeWave += UpdateWave;
        WaveSpawner.OnChangeRoom += UpdateRoomTotalEnmemies;
    }

    private void UpdateWave(Wave wave)
    {
        CurrWave = wave;
        
    }

    public void UpdateRoomTotalEnmemies(int roomTotalEnemies)
    {
        RoomTotalEnemies = roomTotalEnemies;
    }
    
    public void UpdateEnemyCount()
    {
        CurrWave.totalEnemies -= 1;
        RoomTotalEnemies -= 1 ;
    }
}
