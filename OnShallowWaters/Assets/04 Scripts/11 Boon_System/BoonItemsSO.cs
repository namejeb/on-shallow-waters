using UnityEngine;

public enum BoonType {
    COMBAT,
    SURVIVAL,
    BONUS
}

[System.Serializable]
public class BoonItem
{
    public int id;
    public string title;
    public string description;
    public int maxUsageCount;
    public bool isPercentage = true;
    public bool minusHundred = true;
    public Sprite icon;
    public BoonType boonType;
}


[CreateAssetMenu(fileName = "BoonItemsSO", menuName = "ScriptableObjects/BoonItemsSO")]
public class BoonItemsSO : ScriptableObject
{
    public BoonItem[] boonItems;
}

