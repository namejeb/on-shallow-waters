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
    private int _tracker = 0;
    private bool _activated = false;
    
    public bool Activated { get => _activated; set =>_activated = value; }
    public float EffectAmount => effectAmounts[_tracker];
    
    
    public void Upgrade(){
        _tracker++;
    }
}
