
public class BM_AtkSpeed : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseAtkSpd( EffectAmountCurrent );
    }
}
