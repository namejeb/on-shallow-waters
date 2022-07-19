using System.Collections.Generic;
using UnityEngine;

public class BoonsList : MonoBehaviour
{
    public List<Boon> boonList;
  
    
    private void Awake()
    {
       Boon[] foundBoons = transform.GetComponents<Boon>();

       boonList.Clear();
       for (int i = 0; i < foundBoons.Length; i++)
       {
           boonList.Add(foundBoons[i]);
       }
       
       print(boonList[2].boonId);
    }

    public Boon GetBoon(int boonId)
    {
        Boon foundBoon = null;
        for (int i = 0; i < boonList.Count; i++)
        {
            Boon b = boonList[i];
            if (b.boonId == boonId)
            {
                foundBoon = b;
                print(foundBoon.boonId);
            }
        }

        return foundBoon;
    }
}
