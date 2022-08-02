using UnityEngine;
using System;

public class Destination : MonoBehaviour{
    //Tutorial Event
    public static event Action OnMove;

    private void OnTriggerEnter(Collider col){
        if(!col.GetComponent("Player")) return;
        if(OnMove != null) OnMove();
    }
}
