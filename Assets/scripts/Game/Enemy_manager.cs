using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_manager : MonoBehaviour
{
    #region Varaible
    private GameObject target;//Save the tower to hit

    public int health;

    public int enemyCoins;//The coins that give

    public float speed = 16.55f;//His speed

    public NavMeshAgent agente;

    private AudioSource enemyManager;
    public AudioClip walk;
    #endregion

    #region Methots
    void Start()
    {
        //Get the varaible with the component
        enemyManager = GetComponent<AudioSource>();
         agente = GetComponent<NavMeshAgent>();
        
        //Set the tower like the target
        target = GameObject.FindGameObjectWithTag("Finish");
        
        //Sais to the enemy to go to the tower
        agente.destination = target.transform.position;
        agente.speed = speed;
        
        enemyManager.volume = DataPersistence.SharedInfo.effectsGameDP;
        enemyManager.PlayOneShot(walk);
    }

    private void Update()
    {
        LifePoints();
    }
    #endregion

    #region Function

    //Check the life of the enemy
    public void LifePoints()
    {
        //If dies give money
        if(health <= 0)
        {
            GameManager.sharedInstance.UpdateCoins(enemyCoins);
            Destroy(gameObject);
        }
    }
    #endregion 
}
