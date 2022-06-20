using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_{
    public class Summoner : EnemiesCore{
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform[] spawnList;
        public GameObject enemies;
        public bool summonOnce;

        protected override void Start(){
            base.Start();
            
            var size = spawnPoint.childCount;
            spawnList = new Transform[size];
            for(int i = 0; i < size; i++){
                spawnList[i] = spawnPoint.transform.GetChild(i);
            }
        }

        protected override void Attack(){
            UniqueMove();
        }

        protected override void UniqueMove(){
            if (!summonOnce){
                summonOnce = true;
                int spawnPointIndex = Random.Range(0, 3);
                Instantiate(enemies, spawnList[spawnPointIndex].position, spawnList[spawnPointIndex].rotation);
            }
        }
    }
}
