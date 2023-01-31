using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmesh : MonoBehaviour
{
    private GameObject objetivo;

    void Start()
    {
        objetivo = GameObject.FindGameObjectWithTag("Finish");

        NavMeshAgent agente = GetComponent<NavMeshAgent>();
        agente.destination = objetivo.transform.position;
    }

    void Update()
    {
        
    }
}
