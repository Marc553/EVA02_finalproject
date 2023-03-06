using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_torret : MonoBehaviour
{
    public bool withTourret = false;

    private void OnMouseDown()
    {
        if(GameManager.sharedInstance.uIOn == false)
        {
            if (withTourret == false)
            {
                GameManager.sharedInstance.spawnTourretPos = gameObject.transform.position;
                GameManager.sharedInstance.uIOn = true;
            GameManager.sharedInstance.towerSelector.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tourret")
        {
            withTourret = true;
        }
    }
}