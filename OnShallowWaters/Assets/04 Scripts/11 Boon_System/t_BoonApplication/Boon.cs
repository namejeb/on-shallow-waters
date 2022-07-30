using UnityEngine;


/*
 * Categories (and their abbreviations (grouped by how they are applied))
 * BA - Boon Attack (changes outgoingDmg)
 * BS - Boon Survival (variety of application implementation, survivability oriented)
 * BM  - Boon Misc (variety of application implementation)
 */


public class Boon : MonoBehaviour
{
    public int boonId;
    [SerializeField] private float[] effectAmounts;
    private int _upgradeTracker = 0;
    private int _currTracker = -1;
    private bool _activated = false;
    
    public bool Activated { get => _activated; set =>_activated = value; }
    public float EffectAmountToUpgrade { get => effectAmounts[_upgradeTracker]; }
    public float EffectAmountCurrent { get => effectAmounts[_currTracker]; }

    public void Upgrade(){

        if (_upgradeTracker + 1 < effectAmounts.Length )
        {
            _upgradeTracker++;
            _currTracker++;
        }
        else
        {
            _currTracker = _upgradeTracker;
        }
    }
}
