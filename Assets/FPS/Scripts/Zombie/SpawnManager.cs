using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;

namespace Unity.FPS.Zombie
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        
        public GameObject ObjectivePrefab;

        GameObject objective;

        public float spawnInterval = 1; //Spawn new enemy each n seconds
        public int enemiesPerRound = 5; // 0.09 * Round^2 + -0.0029*Round + 23.958 24 max; //How many enemies per Round
        public List<Transform> spawnPoints;
        public int EnemiesEliminated{ get{return enemiesEliminated;} }
        public int RoundNumber{ get{return roundNumber;} }
        public float RoundInterval = 3f;

        float nextSpawnTime = 0;
        int roundNumber = 1;
        bool waitingForRound = true;
        float newRoundTimer = 0;
        int enemiesToEliminate;
        //How many enemies we already eliminated in the current Round
        int enemiesEliminated = 0;
        int totalEnemiesSpawned = 0;

        // Start is called before the first frame update
        void Start()
        {
            //Wait 10 seconds for new Round to start
            newRoundTimer = RoundInterval;
            waitingForRound = true;
            enemiesToEliminate = roundNumber * enemiesPerRound;
            objective = Instantiate(ObjectivePrefab);
            objective.GetComponent<ObjectiveSurvive>().initialize(roundNumber, enemiesToEliminate);
        }

        // Update is called once per frame
        void Update()
        {
            if (waitingForRound)
            {
                if(newRoundTimer >= 0)
                {
                    newRoundTimer -= Time.deltaTime;
                }
                else
                {
                    //Initialize new Round
                    
                    enemiesEliminated = 0;
                    totalEnemiesSpawned = 0;
                    waitingForRound = false;
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
                        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                        GameObject enemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
                        
                        totalEnemiesSpawned++;
                    }
                }
            }
        }

        public void IncrementEnemiesEliminated(){
            enemiesEliminated++;
            if(enemiesEliminated == enemiesToEliminate){
                waitingForRound = true;
                newRoundTimer = RoundInterval;
                roundNumber ++;
                enemiesToEliminate = roundNumber * enemiesPerRound;
                objective.GetComponent<ObjectiveSurvive>().FinishRound();
                objective = Instantiate(ObjectivePrefab);
                objective.GetComponent<ObjectiveSurvive>().initialize(roundNumber, enemiesToEliminate);
            }
        }
    }
}