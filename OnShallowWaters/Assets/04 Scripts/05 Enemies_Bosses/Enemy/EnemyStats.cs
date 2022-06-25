using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private Stat defense;
    
    public Stat Defense { get => defense; }
}
