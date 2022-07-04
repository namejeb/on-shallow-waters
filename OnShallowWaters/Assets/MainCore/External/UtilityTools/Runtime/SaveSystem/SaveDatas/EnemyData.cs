
using UnityEngine;
using NamejebTools.SaveSystem;


[System.Serializable]
public enum EnemyType
{
    CIRCLE,
    SQUARE,
    CAPSULE
}


[System.Serializable]
public class EnemyData 
{
    public string id;
    public EnemyType enemyType;
    public Vector3 EnemyLocation { get; set; }

    public Quaternion EnemyRotation { get; set; }

    public EnemyData(Vector3 location, Quaternion rotation)
    {
        EnemyLocation = location;
        EnemyRotation = rotation;
    }
}