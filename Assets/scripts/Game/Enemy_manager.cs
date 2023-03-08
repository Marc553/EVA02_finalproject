using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_manager : MonoBehaviour
{
    private GameObject target;

    public int health;

    public int enemyCoins;

    public float speed = 16.55f;

    public NavMeshAgent agente;

    private AudioSource enemyManager;
    public AudioClip walk;

    void Start()
    {
        enemyManager = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Finish");

         agente = GetComponent<NavMeshAgent>();
       agente.destination = target.transform.position;
        agente.speed = speed;
        
        enemyManager.volume = DataPersistence.SharedInfo.effectsGameDP;
        enemyManager.PlayOneShot(walk);
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
