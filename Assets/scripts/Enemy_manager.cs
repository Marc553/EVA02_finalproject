using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_manager : MonoBehaviour
{
    private GameObject objetivo;

    public int health;

    public int enemyCoins;

    void Start()
    {
        objetivo = GameObject.FindGameObjectWithTag("Finish");

        NavMeshAgent agente = GetComponent<NavMeshAgent>();
        agente.destination = objetivo.transform.position;

    }

    private void Update()
    {
        LifePoints();
    }

    public void LifePoints()
    {
        if(health <= 0)
        {
            GameManager.sharedInstance.UpdateCoins(enemyCoins);
            Destroy(gameObject);
        }
    }

    

}
