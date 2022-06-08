using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Stats : MonoBehaviour{
    //Attributes
    //public int health;
    //public int damage;
    //public float movement;

    [SerializeField] private Stat health, damage, speed, armour;

    public Stat Speed { get { return speed; } }

    public bool getBuff;

    public void TakeDamage(int dmg){
        //! Armour stats Check
            //! IF Armour reaches O, minus its health and turn into Stun mode [Turn StunOn]
            //! Concept, if(armour <= 0 && !StunOn)
        
        //! IF Health reaches Zero
            //! Trigger Death Animation
    }

    //! LA & HB Behaviours
    // Detect <> Follow <> Attack
    // Health <> Armour
    // Armour <> Recover

    //! FS & SHS Behaviours
    // Detect <> Position <> Line in Sight <> Attack

    //! BS & DS Behaviours
    // BS will heal, provide Extra Armour, increase Damage, Speed
    // DS will poison, decrease armour, decrease Speed and Skill Locking

    // Detect <> Position <> InRange <> Buff/Debuff (AOE)
    // Idle <> Move <> Execute Support

    //! Summoner Behaviours
    // Detect <> Position(Rarely) <> Line in Sight <> Attack
}
