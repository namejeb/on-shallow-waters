
public class BM_ArmorDmgBonus : Boon_Misc
{
    public override void ApplyEffect()
    {
        EnemiesCore.shieldDmgBonus = EffectAmountCurrent;
    }
}
