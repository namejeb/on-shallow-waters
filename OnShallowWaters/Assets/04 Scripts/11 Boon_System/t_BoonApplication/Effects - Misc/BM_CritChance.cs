
public class BM_CritChance : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseCritChance( EffectAmount );
    }
}
