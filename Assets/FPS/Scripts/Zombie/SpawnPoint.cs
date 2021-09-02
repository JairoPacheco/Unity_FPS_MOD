using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Zombie
{
    public class SpawnPoint : MonoBehaviour
    {
        void Start()
        {
            GameObject gameLogic = GameObject.Find("GameLogic");
            gameLogic.GetComponent<SpawnManager>().spawnPoints.Add(this.transform);
        }
    }
}