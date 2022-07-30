
public class BA_SingleEnemyDmgBonus : Boon_Attack
{
    protected override void Awake()
    {
        SetType(DmgModificationType.DMG_ONLY);
    }

    public override float ApplyEffect(float outgoingDmg)
    {
        float finalDmg = outgoingDmg;
        if (WaveSpawner.WaveTotalEnemies == 1)
        {
            finalDmg = outgoingDmg * EffectAmountCurrent;
        }

        return finalDmg;
    }
}
