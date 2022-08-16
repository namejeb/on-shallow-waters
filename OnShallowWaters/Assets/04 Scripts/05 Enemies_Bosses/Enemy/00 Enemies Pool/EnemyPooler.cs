using System.Collections.Generic;
using UnityEngine;


public enum EnemyPoolType { enemy1, enemy2, enemy3 }
public enum ProjectileType { PlayerSKB5, Boss1Shoot, Boss1Slam, Boss1Shield, Enemy3Ball }


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
	public class PickupPoolInfo
	{
		public VFXPickups.PickupType type;
		public int amount;
		public Transform vfxCurrencyPrefab;
		public Transform container;

		public List<Transform> vfxPickupPoolList = new List<Transform>();
	}

	[SerializeField] private List<EnemyPoolInfo> listOfPool;
	[SerializeField] private List<ProjectilePoolInfo> projectilePool;
	[SerializeField] private List<PickupPoolInfo> pickupPool;

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
		
		for (int i = 0; i < pickupPool.Count; i++)
		{
			FillCurrencyPool(pickupPool[i]);
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

	private void FillCurrencyPool(PickupPoolInfo info)
	{
		for (int i = 0; i < info.amount; i++)
		{
			Transform newCurrencyVFX = Instantiate(info.vfxCurrencyPrefab, info.container);
			newCurrencyVFX.gameObject.SetActive(false);
			info.vfxPickupPoolList.Add(newCurrencyVFX);
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
	
	public Transform GetFromPool(VFXPickups.PickupType type)
	{
		PickupPoolInfo pool = GetPoolByType(type);

		for (int i = 0; i < pool.vfxPickupPoolList.Count; i++)
		{
			if (!pool.vfxPickupPoolList[i].gameObject.activeInHierarchy)
			{
				return pool.vfxPickupPoolList[i];
			}
		}

		Transform newCurrencyVFX = Instantiate(pool.vfxCurrencyPrefab, pool.container);
		pool.vfxPickupPoolList.Add(newCurrencyVFX);
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
	
	PickupPoolInfo GetPoolByType(VFXPickups.PickupType poolType)
	{
		for (int i = 0; i < pickupPool.Count; i++)
		{
			if (poolType == pickupPool[i].type)
			{
				return pickupPool[i];
			}
		}

		return null;
	}
}
