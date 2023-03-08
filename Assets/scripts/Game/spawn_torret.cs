using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_torret : MonoBehaviour
{
    #region Variables
    //Bool to check if there is a tourret
    public bool withTourret = false;
    #endregion

    #region Action Functions
    private void OnMouseUp()
    {
        if(GameManager.sharedInstance.uIOn == false)//If the UI of the panel tourret is off
        {
            if (withTourret == false)//If there isn't a tourret, will spawn  
            {
                GameManager.sharedInstance.spawnTourretPos = gameObject.transform.position;//Save the actual fool clicked
                
                //Will set on the panel tourret
                GameManager.sharedInstance.uIOn = true;
            GameManager.sharedInstance.towerSelector.SetActive(true);
            }
        }
    }

    //Check if there is a tourret 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tourret")
        {
            withTourret = true;
        }
    }
    #endregion 
}