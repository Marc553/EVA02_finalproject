using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw_enemies : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] spawnPoints;

    void Update()
    {
        if(GameManager.sharedInstance.onSpawn == true)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);

        spawnPoints = GameManager.sharedInstance.spawnPoints;

        int radomSpawn = Random.Range(0, spawnPoints.Length);

        Instantiate(enemies[randomEnemy], spawnPoints[radomSpawn].transform.position, GameManager.sharedInstance.transform.rotation);
    }
}
