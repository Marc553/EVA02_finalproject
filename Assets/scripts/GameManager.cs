using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tower;
    public GameObject towerSelector;
    public spawn_torret spawn_TorretScript;
    public Vector3 spawnTourretPos;

    public Vector3 mapaPosition = Vector3.zero;
    public Vector3 buttonPosition = new Vector3(0, 15, 0);
    public GameObject expand;



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
        //ExpandStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExpandStart()
    {
        Instantiate(expand, buttonPosition, transform.rotation);
        buttonPosition = buttonPosition - Vector3.forward * 30;
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
