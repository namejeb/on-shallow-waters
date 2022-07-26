using _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_;

public class BM_ArmorDmgBonus : Boon_Misc
{
    public override void ApplyEffect()
    {
        EnemiesCore.shieldDmgBonus = EffectAmount;
    }
}
