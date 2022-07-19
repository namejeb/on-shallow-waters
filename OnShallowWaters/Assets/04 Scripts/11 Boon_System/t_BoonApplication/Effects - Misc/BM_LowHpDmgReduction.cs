
public class BM_LowHpDmgReduction : Boon_Misc
{
    public float DmgReductionActivationThreshold { get; private set; }
    
    public override void ApplyEffect()
    {
        DmgReductionActivationThreshold = EffectAmount;
        //PlayerHandler.Instance.PlayerStats.IncreaseDamageReduction( EffectAmount );
    }
}
