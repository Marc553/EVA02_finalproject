using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_torret : MonoBehaviour
{
    public bool withTourret = false;

    
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnMouseDown()
    {
            GameManager.sharedInstance.spawnTourretPos = gameObject.transform.position;
        if(withTourret == false)
        {
            GameManager.sharedInstance.towerSelector.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tourret")
        {
            withTourret = true;

            Debug.Log("TOCO");

        }
    }


}
