

public class BM_DmgWhenArmorBreak : Boon_Misc
{
    public override void ApplyEffect(EnemyHandler e)
    {
        if (e.EnemiesCore.shieldDestroy)
        {
            e.EnemyStats.Damage( (int) EffectAmount );
        }
    }
}
