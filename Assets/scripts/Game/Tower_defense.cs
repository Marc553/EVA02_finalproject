using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor;

public class Tower_defense : MonoBehaviour
{
    #region Variable
    public int defendedHealth;

    public Animator postProcesingAnim;
    #endregion

    #region Methots
    private void Start()
    {
        //Sets the life at 100
        defendedHealth = 100;
    }
   

    private void Update()
    {
        GameManager.sharedInstance.towerHealth = defendedHealth;//To show the actual life

        if(defendedHealth <= 0)
        {
            defendedHealth = 0;
            GameManager.sharedInstance.GameOver(true);
        }
    }
    #endregion

    #region Function
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            postProcesingAnim.Play("post_procesing");//Shows the hit with postporcesing
            defendedHealth -= other.GetComponent<Enemy_manager>().health;//Lowers the life at the text
            Destroy(other.gameObject);
        }
    }
    #endregion
}
