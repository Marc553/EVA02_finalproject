using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class GameManager : MonoBehaviour
{
    public GameObject tower;
    public GameObject towerSelector;
    public spawn_torret spawn_TorretScript;
    public Vector3 spawnTourretPos;

    public Vector3 mapaPosition = Vector3.zero;
    public Vector3 buttonPosition = new Vector3(0, 15, 0);

    #region Platforms
    public NavMeshSurface surfaces;
    public GameObject[] mapSpawn;

    public GameObject button;

    public GameObject[] spawnPoints;
  
    public Dictionary<int, int[]> platformCombination = new Dictionary<int, int[]>()
    {
        {0, new int[] {2,3} },
        {1, new int[] {2} } ,
        {2, new int[] {0,1, 2} } ,
        {3, new int[] {0} }
    };

    public int oldPlataform = 0;
    #endregion

    public int enemiesLeft;
    private int enemiesPerWave = 2;
    public GameObject[] enemies;

    public bool onSpawn = false;

    public static GameManager sharedInstance;
    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        else 
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
    }

    private void Update()
    {
        enemiesLeft = FindObjectsOfType<navmesh>().Length;
       
        if (enemiesLeft <= 0)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
       
        if(onSpawn == true)
        {
            enemiesPerWave += 3;
            StartCoroutine(SpawnEnemyWave(enemiesPerWave));
            onSpawn = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            button.SetActive(true);
        }
    }

    public void SpawnTower()
    {
        Instantiate(tower, spawnTourretPos, Quaternion.identity);
    }
    public void Exit()
    {
        towerSelector.SetActive(false);
    }
    public void SpawnPlataform()
    {
        int randoPlataform = Random.Range(0, platformCombination[oldPlataform].Length);

        int newPlatform = platformCombination[oldPlataform][randoPlataform];

        Instantiate(mapSpawn[newPlatform], mapaPosition - Vector3.forward * 30, transform.rotation);

        mapaPosition = mapaPosition - Vector3.forward * 30;

        oldPlataform = newPlatform;
    }
    public void MoveButton()
    {
        buttonPosition = buttonPosition - Vector3.forward * 30;
        button.transform.position = buttonPosition;
    }

    private IEnumerator SpawnEnemyWave(int totalenemies)
    {
        for (int i = 0; i < totalenemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1);
        }
    }

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);

        int radomSpawn = Random.Range(0, spawnPoints.Length);

        Instantiate(enemies[randomEnemy], spawnPoints[radomSpawn].transform.position, transform.rotation);
    }


}
