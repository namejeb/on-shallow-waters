using UnityEngine;

public class BM_DmgWhenArmorBreak : Boon_Misc
{
    [TextArea(3, 3)] [SerializeField] private string notes;

    public override void ApplyEffect(EnemyHandler e)
    {
        if (e.EnemiesCore.shieldDestroy)
        {
            e.EnemyStats.Damage( (int) EffectAmountCurrent );
        }
    }
}
