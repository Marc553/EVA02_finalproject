using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tower;
    public GameObject towerSelector;
    public spawn_torret spawn_TorretScript;
    public Vector3 spawnTourretPos;

    

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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTower()
    {
        Instantiate(tower, spawnTourretPos, Quaternion.identity);
    }

    public void Exit()
    {
        towerSelector.SetActive(false);
    }
}
