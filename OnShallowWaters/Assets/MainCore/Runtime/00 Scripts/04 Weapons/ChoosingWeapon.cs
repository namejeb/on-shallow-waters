using UnityEngine;

[CreateAssetMenu(fileName = "weapon name", menuName = "New Weapon")]
public class ChoosingWeapon : ScriptableObject
{
    [Header("Description")]
    public string weaponName;
    public string weaponDescription;

    [Header("Stats")]
    [Range(1,10)]public float weaponDamage;
    [Range(1, 10)]public float weaponDamageRange;
    [Range(1, 10)] public float weaponDamageSpeed;

    [Header("3D Model")]
    public GameObject weaponModel;


}
