
public class BM_MaxHp : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseMaxHp( EffectAmountCurrent );
    }
}
