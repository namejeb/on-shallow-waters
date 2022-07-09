using UnityEngine;

[System.Serializable]
public class BoonItem
{
    public int id;
    public string title;
    public string description;
    public int maxUsageCount;
    public bool isPercentage = true;
}


[CreateAssetMenu(fileName = "BoonItemsSO", menuName = "ScriptableObjects/BoonItemsSO")]
public class BoonItemsSO : ScriptableObject
{
    public BoonItem[] boonItems;
}

