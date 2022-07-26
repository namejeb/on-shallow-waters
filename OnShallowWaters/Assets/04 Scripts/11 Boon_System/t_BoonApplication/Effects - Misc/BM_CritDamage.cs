
public class BM_CritDamage : Boon_Misc
{
    public override void ApplyEffect()
    {
        PlayerHandler.Instance.PlayerStats.IncreaseCritDmg( EffectAmount );
    }
}
