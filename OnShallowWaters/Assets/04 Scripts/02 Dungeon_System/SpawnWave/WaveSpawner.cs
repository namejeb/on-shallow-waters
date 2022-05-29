using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { NOTHING, SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Enemies
    {
        public Transform enemy1;
        public int count1;
        public Transform enemy2;
        public int count2;
        public Transform enemy3;
        public int count3;
        public Transform enemy4;
        public int count4;
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Enemies enemies;
        //public float rate;
    }

    [Header("Wave Settings")]
    [SerializeField] private float waveCounddown;
    [SerializeField] private float waveIntervalTime = 3f;
    [SerializeField] private List<Transform> spawnPoints;
    private float _searchCountdown = 1f;
    private int _nextWave = 0;
    private bool isEnd;

    [Header("Enemy Initialization")]
    public Wave[] waves;

    [Header("Game State")]
    public SpawnState state = SpawnState.NOTHING;
	private DoorTrigger dt;

    private void Awake()
    {
        dt = GetComponentInChildren<DoorTrigger>();
    }

    private void Start()
    {
        waveCounddown = waveIntervalTime;
    }

    private void Update()
    {
		// Spawn start when player step into room
        if (state == SpawnState.NOTHING && dt.doorTriggerd && !isEnd)
        {
            state = SpawnState.WAITING;
        }

		// When state is WAITING, if enemy dead, either [start next wave] or [end stage] 
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                if (isEnd)
                {
                    state = SpawnState.NOTHING;
                    dt.SetWallStatus(false);
                }
                else
                    WaveCompleted(waves[_nextWave]);
            }
            else
                return;
        }
		
		// Rest time between wave
        if (waveCounddown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpwanWave(waves[_nextWave]));
            }
        }
        else if (state == SpawnState.NOTHING)
            return;
        else
            waveCounddown -= Time.deltaTime;
    }

    void WaveCompleted(Wave wave)
    {
        state = SpawnState.COUNTING;
        waveCounddown = waveIntervalTime;
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
	
    IEnumerator SpwanWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

		SpawnLoop(_wave.enemies.count1, _wave.enemies.enemy1);
		SpawnLoop(_wave.enemies.count2, _wave.enemies.enemy2);
		SpawnLoop(_wave.enemies.count3, _wave.enemies.enemy3);
		SpawnLoop(_wave.enemies.count4, _wave.enemies.enemy4);
		
		// Determine are every waves go through already or not
        if (_nextWave < waves.Length - 1)
            _nextWave++;
        else
        {
            waveCounddown = 5;
            isEnd = true;
        }

        state = SpawnState.WAITING;
        yield break;
    }
	
	void SpawnLoop(int waveCount, Transform enemy)
	{
		for (int i = 0; i < waveCount; i++)
		{
			SpawnEnemy(enemy);
		}
	}

    void SpawnEnemy(Transform _enemy)
    {
        // Random Position in square

        int spawnIndex = Random.Range(0, spawnPoints.Count);
		
		//Spawn enemy (not Object Pooler)
        //Transform enemy = Instantiate(_enemy, pos, transform.rotation);
		
		//Spawn enemy (Object Pooler)
		// Transform enemy = enemyPooler.GetFromPool();
		// enemy.position = spawnPoints[spawnIndex];
    }
}
