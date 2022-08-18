using UnityEngine;

public class TBreakableProps : BreakableProp
{
    protected override void Break()
    {
        transform.parent.GetComponent<BreakableProps_Tutorial>().UpdatePropsToBreak();
        
        //play sound
      
        //function
        if (Dropped)
        {
            HealPlayer();
        }
        
        //visual
  
        Vector3 rot = transform.eulerAngles;
        Vector3 rotOffset = new Vector3(90f, 0f, 0f);
        Quaternion targetRot = Quaternion.Euler(rot + rotOffset);

        Instantiate(brokenVerPrefab, transform.position, targetRot);
        SetPropActive(false);
        SoundManager.instance.PlaySFX(BreakableSFX,"Barrel SFX");
    }
}
