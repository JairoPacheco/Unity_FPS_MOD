using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.AI
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        //public SC_DamageReceiver player;
        public float spawnInterval = 1; //Spawn new enemy each n seconds
        public int enemiesPerWave = 5; // 0.09 * Round^2 + -0.0029*Round + 23.958 24 max; //How many enemies per wave
        public Transform[] spawnPoints;
        public int EnemiesEliminated{ get{return enemiesEliminated;} }

        float nextSpawnTime = 0;
        int waveNumber = 1;
        bool waitingForWave = true;
        float newWaveTimer = 0;
        int enemiesToEliminate;
        //How many enemies we already eliminated in the current wave
        int enemiesEliminated = 0;
        int totalEnemiesSpawned = 0;

        // Start is called before the first frame update
        void Start()
        {
            //Wait 10 seconds for new wave to start
            newWaveTimer = 10;
            waitingForWave = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (waitingForWave)
            {
                if(newWaveTimer >= 0)
                {
                    newWaveTimer -= Time.deltaTime;
                }
                else
                {
                    //Initialize new wave
                    enemiesToEliminate = waveNumber * enemiesPerWave;
                    enemiesEliminated = 0;
                    totalEnemiesSpawned = 0;
                    waitingForWave = false;
                }
            }
            else
            {
                if(Time.time > nextSpawnTime)
                {
                    nextSpawnTime = Time.time + spawnInterval;

                    //Spawn enemy 
                    if(totalEnemiesSpawned < enemiesToEliminate)
                    {
                        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];

                        GameObject enemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
                        
                        totalEnemiesSpawned++;
                    }
                }
            }
        }

        public void IncrementEnemiesEliminated(){
            enemiesEliminated++;
            if(enemiesEliminated == enemiesToEliminate){
                waitingForWave = true;
                newWaveTimer = 5;
            }
        }
    }
}