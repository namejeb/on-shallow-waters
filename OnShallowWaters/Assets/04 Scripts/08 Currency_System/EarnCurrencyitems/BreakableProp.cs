using _04_Scripts._05_Enemies_Bosses;
using UnityEngine;

public class BreakableProp : MonoBehaviour, IDamageable
{
    public void Damage(int damageAmount)
    {
        Break();
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }

    private void Break()
    {
        //play sound
        gameObject.SetActive(false);
    }
}
