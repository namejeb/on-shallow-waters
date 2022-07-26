
public class BM_AtkPercent : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseAtkPercent( EffectAmount );
    }
}
