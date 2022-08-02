using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public class LightAttacker : EnemiesCore {
        public bool missAttack;
        private bool _noRepeat;
        public int targetHit;

        protected override void Start(){
            base.Start();
            targetHit = 0;
        }

        protected override void Attack() {
            if (!missAttack && targetHit != 2){
                anim.SetBool("isWalk", false);
                switch(targetHit){
                    case 0: anim.SetTrigger("isAttack1"); break;
                    case 1: anim.SetTrigger("isAttack2"); break;
                }
            } else{
                StartCoroutine(waitThenChase());
            }
        }

        private IEnumerator waitThenChase(){
            yield return new WaitForSeconds(1.5f);
            behaviour = CoreStage.Move;
            missAttack = false;
            _noRepeat = false;
            targetHit = 0;
        }
    }
}
