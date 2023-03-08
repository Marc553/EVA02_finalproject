using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_button : MonoBehaviour
 {
    #region Methots
    private void Start()
    {
        transform.position = GameManager.sharedInstance.buttonPosition;//Take the current position
    }
    #endregion

    #region Function
    private void OnMouseDown()
    {

        GameManager.sharedInstance.MoveButton();//Move the button to the new position
        GameManager.sharedInstance.SpawnPlataform();//Spawn the floor
        GameManager.sharedInstance.surfaces.BuildNavMesh();//Spawn the new navmesh
        foreach(GameObject sP in GameManager.sharedInstance.spawnPoints)//Off the inital point
        {
            sP.SetActive(false);
        }
        GameManager.sharedInstance.spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");//Set the new point 
        GameManager.sharedInstance.onSpawn = true;//Active the spawn
    }
    #endregion 
}
