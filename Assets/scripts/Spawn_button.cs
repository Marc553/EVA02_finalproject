using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_button : MonoBehaviour
 {
    private void Start()
    {
        transform.position = GameManager.sharedInstance.buttonPosition;
    }
    private void OnMouseDown()
    {

        GameManager.sharedInstance.MoveButton();
        GameManager.sharedInstance.SpawnPlataform();
        GameManager.sharedInstance.surfaces.BuildNavMesh();
        foreach(GameObject sP in GameManager.sharedInstance.spawnPoints)
        {
            sP.SetActive(false);
        }
        GameManager.sharedInstance.spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        GameManager.sharedInstance.onSpawn = true;
    }


}
