

public enum DmgModificationType
{
    DMG_ONLY,
    DMG_ENEMY,
}

public class Boon_Attack : Boon
{
    private DmgModificationType type = DmgModificationType.DMG_ENEMY;
    public DmgModificationType Type => type;

    // Set DmgModificationType on Awake() based on arguments of ApplyEffect() to work properly
    protected virtual void Awake() { }

    public virtual float ApplyEffect(float outgoingDmg)
    {
        return 0f;
    }

    public virtual float ApplyEffect(float outgoingDmg, EnemyHandler e)
    {
        return 0f;
    }

    protected void SetType(DmgModificationType t)
    {
        type = t;
    }
}
