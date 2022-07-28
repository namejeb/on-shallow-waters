using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
	public enum EnemyPoolType {enemy1, enemy2}
	
	[System.Serializable]
	public class PoolInfo
	{
		public EnemyPoolType type;
		public int amount;
		public Transform enemyPrefab;
		public Transform container;
		
		public List<Transform> enemyPoolList = new List<Transform>();
	}
	
	[SerializeField] private List<PoolInfo> listOfPool;

	public static EnemyPooler Instance;
	private void Awake()
	{
		Instance = this;
	}


	void Start()
	{
		for (int i = 0; i < listOfPool.Count; i++)
		{
			FillPool(listOfPool[i]);
		}
	}
	
	void FillPool(PoolInfo info)
	{ 
		for (int i = 0; i < info.amount; i++)
		{
			Transform newEnemy = Instantiate(info.enemyPrefab, info.container);
			newEnemy.gameObject.SetActive(false);
			info.enemyPoolList.Add(newEnemy);
		}
	}
	
	
	public Transform GetFromPool(EnemyPoolType type)
	{
		PoolInfo pool = GetPoolByType(type);

		for (int i = 0; i < pool.enemyPoolList.Count; i++)
		{
			if (!pool.enemyPoolList[i].gameObject.activeInHierarchy)
			{
				return pool.enemyPoolList[i];
			}
		}

		Transform newEnemy = Instantiate(pool.enemyPrefab, pool.container);
		pool.enemyPoolList.Add(newEnemy);
		return newEnemy;
	}
	
	PoolInfo GetPoolByType(EnemyPoolType poolType)
	{
		for (int i = 0; i < listOfPool.Count; i++)
		{
			if (poolType == listOfPool[i].type)
			{
				return listOfPool[i];
			}
		}
		
		return null;
	}
}
