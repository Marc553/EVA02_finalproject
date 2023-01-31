using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tower;
    public GameObject towerSelector;
    public spawn_torret spawn_TorretScript;

    void Start()
    {
        spawn_TorretScript = FindObjectOfType<spawn_torret>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTower()
    {
        Instantiate(tower, spawn_TorretScript.pos.position, spawn_TorretScript.pos.transform.rotation);
    }

    public void Exit()
    {
        towerSelector.SetActive(false);
    }
}
