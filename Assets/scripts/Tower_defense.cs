using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_defense : MonoBehaviour
{
    public int defendedHealth;

    private void Start()
    {
        defendedHealth = 100;
    }

    private void Update()
    {
        GameManager.sharedInstance.towerHealth = defendedHealth;

        if(defendedHealth <= 0)
        {
            GameManager.sharedInstance.GameOver(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            defendedHealth -= other.GetComponent<Enemy_manager>().health;
            Destroy(other.gameObject);
        }
    }

}
