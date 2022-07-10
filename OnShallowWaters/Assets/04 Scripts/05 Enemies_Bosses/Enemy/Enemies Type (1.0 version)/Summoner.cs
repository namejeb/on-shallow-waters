using UnityEngine;

namespace _04_Scripts._05_Enemies_Bosses.Enemy.Enemies_Type__1._0_version_{
    public class Summoner : EnemiesCore{
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform[] spawnList;
        public GameObject enemies;

        //! Fire Point
        public Transform firePoint;
        [SerializeField] private GameObject effectToSpawn;
        [SerializeField] private float timeToFire;
        private EnemiesProjectile _enemiesProjectile;
        private bool _readyToSummon;


        [SerializeField] private float timeToSummon;
        private Quaternion _rotate1;
        private Vector3 _direction1;
        private Vector3 _pos1;

        public int setBehaviour;
        public int hasFired;
        public int firedNum;
        public int summonNum;

        protected override void Start(){
            base.Start();

            _readyToSummon = true;
            _enemiesProjectile = effectToSpawn.GetComponent<EnemiesProjectile>();
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
                behaviour = CoreStage.Move;
                return;
            }

            switch (setBehaviour){
                case 2 when _readyToSummon: UniqueMove(); break;
                default:  Fire(); break;
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

        private void Fire(){
            Transform trans;
            
            (trans = transform).LookAt(puppet);
            _direction1 = puppet.position - trans.position;
            _rotate1 = Quaternion.LookRotation(_direction1);

            if (Time.time >= timeToFire){
                timeToFire = Time.time + (1 / _enemiesProjectile.fireRate);
                Quaternion rotation = _rotate1;
                Instantiate(effectToSpawn, firePoint.position, rotation);
                firedNum += 1;
            }
            
            if (firedNum != 3) return;
            behaviour = CoreStage.Move;
            firedNum = 0;
            
            if (!_readyToSummon){
                hasFired += 1;
            }
        }
    }
}
