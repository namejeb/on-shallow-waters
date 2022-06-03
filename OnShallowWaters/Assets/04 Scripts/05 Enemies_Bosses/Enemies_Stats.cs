using UnityEngine;

public class Enemies_Stats : MonoBehaviour{
    //Attributes
    public int health;
    public int damage;
    public float movement;

    public bool getBuff;

    public void ReceivedBuff(int type, int num){
        //! Health Recover
        //! Speed Increase
        //! Armour Gain / Armour Recover
        //! Damage Enhance
    }

    public void TakeDamage(int dmg){
        //! Armour stats Check
            //! IF Armour reaches O, minus its health and turn into Stun mode [Turn StunOn]
            //! Concept, if(armour <= 0 && !StunOn)
        
        //! IF Health reaches Zero
            //! Trigger Death Animation
    }
}
