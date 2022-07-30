
public class BM_Defense : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseDef ( EffectAmountCurrent );
    }
}
