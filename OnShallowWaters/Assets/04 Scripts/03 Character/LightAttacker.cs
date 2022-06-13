using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttacker : Enemies_Core{
    void Awake(){
        behaviour = core_stage.Move;
    }

    protected override void Movement(){
        base.Movement();
    }
}
