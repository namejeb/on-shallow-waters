using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BoonItem
{
    public int id;
    public string title;
    public string description;
    public int maxUsageCount;
    public bool isPercentage = true;
    
    [Space]
    [Header("[!] 0-single val, 1-stat array, 2-float array")]
    public int increaseAmountType = 1;
}


[CreateAssetMenu(fileName = "BoonItemsSO", menuName = "ScriptableObjects/BoonItemsSO")]
public class BoonItemsSO : ScriptableObject
{
    public BoonItem[] boonItems;
}

