
using UnityEngine;

public class BM_LowHpDmgReduction : Boon_Misc
{
    [TextArea(3, 3)] [SerializeField] private string notes;
    
    public float DmgReductionActivationThreshold { get; private set; }
    
    public override void ApplyEffect()
    {
        DmgReductionActivationThreshold = EffectAmount;
        //PlayerHandler.Instance.PlayerStats.IncreaseDamageReduction( EffectAmount );
    }
}
