using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Swpan_area : MonoBehaviour
{
    public NavMeshSurface surfaces;
    public GameObject mapa;
    public GameObject mapaPosition;
    public GameObject enemy;
    public GameObject enemyPosition;

    public bool left;
    public bool right;
    public bool inway;
    public bool outway;

    // Use this for initialization
    void Update()
    {
    }

    public void SpawnManager()
    {
        
        Instantiate(mapa, mapaPosition.transform.position, mapaPosition.transform.rotation);
    }public void SpawnEnemy()
    {
        Instantiate(enemy, enemyPosition.transform.position, enemyPosition.transform.rotation);
    }

    private void OnMouseDown()
    {
            SpawnManager();
            surfaces.BuildNavMesh();
            SpawnEnemy();
        
        gameObject.SetActive(false);
    }

}

