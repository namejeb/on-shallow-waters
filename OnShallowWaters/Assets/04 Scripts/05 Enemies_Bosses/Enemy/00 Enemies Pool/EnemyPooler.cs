using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPooler : MonoBehaviour
{
	public enum EnemyPoolType { enemy1, enemy2 }
	public enum ProjectileType { e1p1, e2p1, b1p1 }
	
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

	[SerializeField] private List<EnemyPoolInfo> listOfPool;
	[SerializeField] private List<ProjectilePoolInfo> projectilePool;

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

	public Transform GetFromPool(EnemyPoolType type)
	{
		EnemyPoolInfo pool = GetPoolByType(type);

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
}
