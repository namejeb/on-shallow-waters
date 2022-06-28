using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private EnemyStats _enemyStats;
    private EnemiesCore _enemiesCore;

    public EnemyStats EnemyStats { get => _enemyStats; }
    public EnemiesCore EnemiesCore { get => _enemiesCore; }

    private new void  Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _enemiesCore = GetComponent<EnemiesCore>();
    }
}
