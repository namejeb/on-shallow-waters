using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_{
    public class Summoner : SlowHeavyShooter{
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform[] spawnList;
        public GameObject enemies;
        private bool _readyToSummon;

        [SerializeField] private float timeToSummon;
        private int setBehaviour;
        private int hasFired;
        private int summonNum;

        protected override void Start(){
            base.Start();
            _readyToSummon = true;

            var size = spawnPoint.childCount;
            spawnList = new Transform[size];
            for(int i = 0; i < size; i++){
                spawnList[i] = spawnPoint.transform.GetChild(i);
            }
        }

        protected override void Movement(){
            if (hasFired == 3){
                _readyToSummon = true;
                hasFired = 0;
            }
            
            if (dist < radius * radius){
                setBehaviour = Random.Range(0, 3);
                behaviour = CoreStage.Attack;
            }
        }

        protected override void Attack(){
            if (dist > radius * radius){
                behaviour = CoreStage.Idle;
                return;
            }

            switch (setBehaviour){
                case 2 when _readyToSummon: UniqueMove(); break;
                default:  FireBullet(); break;
            }
        }

        protected override void UniqueMove(){
            if (Time.time >= timeToSummon){
                timeToSummon = Time.time + 1;
                if (summonNum < 4){
                    Instantiate(enemies, spawnList[summonNum].position, spawnList[summonNum].rotation);
                    summonNum += 1;
                } else {
                    summonNum = 0;
                    _readyToSummon = false;
                    behaviour = CoreStage.Move;
                }
            }

        }

        protected override void FireBullet(){
            base.FireBullet();
            
            if (!_readyToSummon){
                hasFired += 1;
            }
        }
        
    }
}
