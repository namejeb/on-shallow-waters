using UnityEngine;

public class TutorialTracker : MonoBehaviour
{
    public int numAttack;
    enum Stage{
        move, dash, attack, puppet, exit
    }

    void Start(){
        DashNAttack.OnAttack += Attack;
        DashNAttack.OnDash += Dash;
        Destination.OnMove += Move; 
    }

    

    public void Dash(){

    }

    public void Move(){

    }

    public void Attack(){
        if(numAttack != 3) return;
        numAttack++;
    }

    void OnDestroy(){
        DashNAttack.OnAttack -= Attack;
        DashNAttack.OnDash -= Dash;
        Destination.OnMove -= Move; 
    }
}
