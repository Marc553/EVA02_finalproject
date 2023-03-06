using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor;

public class Tower_defense : MonoBehaviour
{
    public int defendedHealth;

    public Animator postProcesingAnim; 

    private void Start()
    {
        defendedHealth = 100;
    }

    private void Update()
    {
        GameManager.sharedInstance.towerHealth = defendedHealth;

        if(defendedHealth <= 0)
        {
            defendedHealth = 0;
            GameManager.sharedInstance.GameOver(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            postProcesingAnim.Play("post_procesing");
            defendedHealth -= other.GetComponent<Enemy_manager>().health;
            Destroy(other.gameObject);
        }
    }

}
