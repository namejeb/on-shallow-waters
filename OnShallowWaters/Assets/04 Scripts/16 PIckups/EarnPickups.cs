using UnityEngine;

public class EarnPickups : MonoBehaviour
{
    [SerializeField] private VFXPickups.PickupType type;
    [SerializeField] protected Vector2Int minMaxAmount;

    public VFXPickups.PickupType Type
    {
        get => type;
    }
}
