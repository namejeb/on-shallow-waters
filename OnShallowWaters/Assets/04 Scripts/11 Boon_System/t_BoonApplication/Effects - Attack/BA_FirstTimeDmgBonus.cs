

public class BA_FirstTimeDmgBonus : Boon_Attack
{
    protected override void Awake()
    {
        SetType(DmgModificationType.DMG_ENEMY);
    }

    public override float ApplyEffect(float outgoingDmg, EnemyHandler e)
    {
        float finalDmg = outgoingDmg; 
        if (e.EnemyStats.CurrHpPercentage >= 1f)
        {
            finalDmg = outgoingDmg * (1 + EffectAmount);
        }

        return finalDmg;
    }
}
