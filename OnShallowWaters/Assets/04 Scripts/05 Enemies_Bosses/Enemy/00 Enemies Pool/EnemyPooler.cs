using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyPoolType { enemy1, enemy2, enemy3 }
public enum ProjectileType { PlayerSKB5, Boss1Shoot, Boss1Slam, Enemy3Ball }
public enum VFXCurrencyType { Gold, Soul }

public class EnemyPooler : MonoBehaviour
{

	[System.Serializable]
	public class EnemyPoolInfo
	{
		public EnemyPoolType type;
		public int amount;
		public Transform enemyPrefab;
		public Transform container;
		
		public List<Transform> enemyPoolList = new List<Transform>();
	}

	[System.Serializable]
	public class ProjectilePoolInfo
	{
		public ProjectileType type;
		public int amount;
		public Transform projectilePrefab;
		public Transform container;

		public List<Transform> projectilePoolList = new List<Transform>();
	}
	
	[System.Serializable]
	public class CurrencyPoolInfo
	{
		public VFXCurrencyType type;
		public int amount;
		public Transform vfxCurrencyPrefab;
		public Transform container;

		public List<Transform> vfxCurrencyPoolList = new List<Transform>();
	}

	[SerializeField] private List<EnemyPoolInfo> listOfPool;
	[SerializeField] private List<ProjectilePoolInfo> projectilePool;
	[SerializeField] private List<CurrencyPoolInfo> currencyPool;

	public static EnemyPooler Instance;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		for (int i = 0; i < listOfPool.Count; i++)
		{
			FillEnemyPool(listOfPool[i]);
		}

		for (int i = 0; i < projectilePool.Count; i++)
		{
			FillProjectilePool(projectilePool[i]);
		}
		
		for (int i = 0; i < currencyPool.Count; i++)
		{
			FillCurrencyPool(currencyPool[i]);
		}
	}
	
	void FillEnemyPool(EnemyPoolInfo info)
	{ 
		for (int i = 0; i < info.amount; i++)
		{
			Transform newEnemy = Instantiate(info.enemyPrefab, Vector3.zero, Quaternion.identity, info.container);
			newEnemy.gameObject.SetActive(false);
			info.enemyPoolList.Add(newEnemy);
		}
	}

	void FillProjectilePool(ProjectilePoolInfo info)
	{
		for (int i = 0; i < info.amount; i++)
		{
			Transform newEnemy = Instantiate(info.projectilePrefab, info.container);
			newEnemy.gameObject.SetActive(false);
			info.projectilePoolList.Add(newEnemy);
		}
	}

	private void FillCurrencyPool(CurrencyPoolInfo info)
	{
		for (int i = 0; i < info.amount; i++)
		{
			Transform newCurrencyVFX = Instantiate(info.vfxCurrencyPrefab, info.container);
			newCurrencyVFX.gameObject.SetActive(false);
			info.vfxCurrencyPoolList.Add(newCurrencyVFX);
		}
	}

	public Transform GetFromPool(EnemyPoolType type, Vector3 position)
	{
		EnemyPoolInfo pool = GetPoolByType(type);

		for (int i = 0; i < pool.enemyPoolList.Count; i++)
		{
			if (!pool.enemyPoolList[i].gameObject.activeInHierarchy)
			{
				return pool.enemyPoolList[i];
			}
		}

		Transform newEnemy = Instantiate(pool.enemyPrefab, position, Quaternion.identity, pool.container);
		pool.enemyPoolList.Add(newEnemy);
		return newEnemy;
	}

	public Transform GetFromPool(ProjectileType type)
	{
		ProjectilePoolInfo pool = GetPoolByType(type);

		for (int i = 0; i < pool.projectilePoolList.Count; i++)
		{
			if (!pool.projectilePoolList[i].gameObject.activeInHierarchy)
			{
				return pool.projectilePoolList[i];
			}
		}

		Transform newProjectile = Instantiate(pool.projectilePrefab, pool.container);
		pool.projectilePoolList.Add(newProjectile);
		return newProjectile;
	}
	
	public Transform GetFromPool(VFXCurrencyType type)
	{
		CurrencyPoolInfo pool = GetPoolByType(type);

		for (int i = 0; i < pool.vfxCurrencyPoolList.Count; i++)
		{
			if (!pool.vfxCurrencyPoolList[i].gameObject.activeInHierarchy)
			{
				return pool.vfxCurrencyPoolList[i];
			}
		}

		Transform newCurrencyVFX = Instantiate(pool.vfxCurrencyPrefab, pool.container);
		pool.vfxCurrencyPoolList.Add(newCurrencyVFX);
		return newCurrencyVFX;
	}

	EnemyPoolInfo GetPoolByType(EnemyPoolType poolType)
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

	ProjectilePoolInfo GetPoolByType(ProjectileType poolType)
	{
		for (int i = 0; i < projectilePool.Count; i++)
		{
			if (poolType == projectilePool[i].type)
			{
				return projectilePool[i];
			}
		}

		return null;
	}
	
	CurrencyPoolInfo GetPoolByType(VFXCurrencyType poolType)
	{
		for (int i = 0; i < currencyPool.Count; i++)
		{
			if (poolType == currencyPool[i].type)
			{
				return currencyPool[i];
			}
		}

		return null;
	}
}
