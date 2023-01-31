using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_torret : MonoBehaviour
{
    public GameObject towerSelector;
    public Transform pos;
   
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnMouseDown()
    {
            pos = gameObject.transform;
            towerSelector.SetActive(true);
    }
}
