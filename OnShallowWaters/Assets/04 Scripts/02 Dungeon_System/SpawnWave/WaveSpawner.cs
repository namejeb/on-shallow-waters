using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { NOTHING, SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Enemies
    {
        public EnemyPooler.EnemyPoolType enemy;
        public int count;
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<Enemies> enemies;
        public int totalEnemies = 0;
        //public float rate;

        public bool IsWaveComplete => totalEnemies == 0;

        public void Awake()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                totalEnemies += enemies[i].count;
            }
        }
    }

    [Header("Wave Settings")]
    [SerializeField] private float waveCountdown;
    [SerializeField] private float waveIntervalTime = 3f;
    private List<Transform> _spawnPoints = new List<Transform>();

    private static int _roomTotalEnemies = 0;
    public static bool IsLastEnemy => _roomTotalEnemies == 0;
    
    private EnemyPooler _enemyPooler;
    
    private int _nextWave = 0;
    private bool _isEnd;
    
    private static Wave _currentWave;

    [Header("Enemy Initialization")]
    public Wave[] waves;

    [Header("Game State")]
    public SpawnState state = SpawnState.NOTHING;
    
    private void Start()
    {
        _spawnPoints.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints.Add(transform.GetChild(i));
        }
        
        _enemyPooler = EnemyPooler.Instance;
        waveCountdown = 0f;

        _currentWave = waves[0];

        for (int i = 0; i < waves.Length; i++)
        {
            for (int j = 0; j < waves[i].enemies.Count; j++)
            {
                _roomTotalEnemies += waves[i].enemies[j].count;
            }
        }
    }

    private void Update()
    {
		// Spawn start when player step into room
        if (state == SpawnState.NOTHING && !_isEnd)
        {
            state = SpawnState.WAITING;
        }

		// When state is WAITING, if enemy dead, either [start next wave] or [end stage] 
        if (state == SpawnState.WAITING)
        {
            if (_currentWave.IsWaveComplete)
            {
                if (_isEnd) state = SpawnState.NOTHING;
                else WaveCompleted(waves[_nextWave]);
            }
            else return;
        }
		
		// Rest time between wave
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING) StartCoroutine(SpawnWave(waves[_nextWave]));
        }
        else if (state == SpawnState.NOTHING) return;
        else waveCountdown -= Time.deltaTime;
    }

    void WaveCompleted(Wave wave)
    {
        state = SpawnState.COUNTING;
        waveCountdown = waveIntervalTime;
    }
    
    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        wave.Awake();
        _currentWave = wave;
        
		for (int i = 0; i < wave.enemies.Count; i++)
		{
			for (int j = 0; j < wave.enemies[i].count; j++)
            {
                 SpawnEnemy(wave.enemies[i].enemy);
            }
		}
		
		// Determine are every waves go through already or not
        if (_nextWave < waves.Length - 1)
            _nextWave++;
        else
        {
            waveCountdown = 5;
            _isEnd = true;
        }

        state = SpawnState.WAITING;
        yield break;
    }
	
    void SpawnEnemy(EnemyPooler.EnemyPoolType enemyType)
    {
        // Random Spawn points
        int spawnIndex = Random.Range(0, _spawnPoints.Count);

		//Spawn enemy (Object Pooling)
		Transform e = _enemyPooler.GetFromPool(enemyType);
        e.gameObject.SetActive(true);
		e.position = _spawnPoints[spawnIndex].position;
    }

    public static void UpdateWaveTotalEnemies()
    {
        _currentWave.totalEnemies -= 1;
        _roomTotalEnemies -= 1 ;
    }

    public static int GetCurrWaveTotalEnemies()
    {
        print(_currentWave.totalEnemies);
        return _currentWave.totalEnemies;
    }
}
