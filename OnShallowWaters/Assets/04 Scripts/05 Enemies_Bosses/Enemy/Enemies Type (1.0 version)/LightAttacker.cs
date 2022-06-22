using System.Collections;
using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_ {
    public class LightAttacker : EnemiesCore {
        public Animator anim;
        
        public bool missAttack;
        private bool _noRepeat;
        public int targetHit;

        protected override void Start(){
            base.Start();
            targetHit = 0;
        }

        protected override void Attack() {
            if (!missAttack && targetHit != 1){
                if (_noRepeat) return;
                _noRepeat = true;
                anim.SetTrigger("secondAtt");
                anim.SetTrigger("firstAtt");
            } else{
                StartCoroutine(waitThenChase());
            }
        }

        private IEnumerator waitThenChase(){
            yield return new WaitForSeconds(1.5f);
            behaviour = CoreStage.Move;
            missAttack = _noRepeat = false;
            targetHit = 0;
        }
    }
}