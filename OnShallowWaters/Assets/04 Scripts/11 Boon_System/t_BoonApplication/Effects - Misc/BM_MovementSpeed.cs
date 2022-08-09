
public class BM_MovementSpeed : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseMvmntSpd( EffectAmountCurrent );
    }
}
