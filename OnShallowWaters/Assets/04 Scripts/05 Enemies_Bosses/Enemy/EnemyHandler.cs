using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;
using UnityEngine;


public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private bool isShielded;
    
    private EnemyStats _enemyStats;
    private EnemiesCore _enemiesCore;
    private LightAttacker _lightAttacker;
    

    public EnemyStats EnemyStats { get => _enemyStats; }
    public EnemiesCore EnemiesCore { get => _enemiesCore; }

    private new void  Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();

        if (isShielded)
            _enemiesCore = GetComponent<EnemiesCore>();
        else
            _lightAttacker = GetComponent<LightAttacker>();
    }
}
