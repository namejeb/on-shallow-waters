using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //public float rate;
    }

    [Header("Wave Settings")]
    [SerializeField] private float waveCountdown;
    [SerializeField] private float waveIntervalTime = 3f;
    [SerializeField] private List<Transform> spawnPoints;
    private EnemyPooler _enemyPooler;
    private float _searchCountdown = 1f;
    private int _nextWave = 0;
    private bool _isEnd;

    [Header("Enemy Initialization")]
    public Wave[] waves;

    [Header("Game State")]
    public SpawnState state = SpawnState.NOTHING;
    
    private void Start()
    {
        _enemyPooler = EnemyPooler.Instance;
        waveCountdown = 0f;
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
            if (!EnemyIsAlive())
            {
                if (_isEnd)
                {
                    state = SpawnState.NOTHING;
                }
                else
                    WaveCompleted(waves[_nextWave]);
            }
            else
                return;
        }
		
		// Rest time between wave
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[_nextWave]));
            }
        }
        else if (state == SpawnState.NOTHING)
            return;
        else
            waveCountdown -= Time.deltaTime;
    }

    void WaveCompleted(Wave wave)
    {
        state = SpawnState.COUNTING;
        waveCountdown = waveIntervalTime;
    }

    bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;
        if (_searchCountdown <= 0)
        {
            _searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }
        return true;
    }
	
    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

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
        int spawnIndex = Random.Range(0, spawnPoints.Count);

		//Spawn enemy (Object Pooling)
		Transform e = _enemyPooler.GetFromPool(enemyType);
        e.gameObject.SetActive(true);
		e.position = spawnPoints[spawnIndex].position;
    }
}
