using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmesh : MonoBehaviour
{
    public Transform objetivo;

    void Start()
    {
        NavMeshAgent agente = GetComponent<NavMeshAgent>();
        agente.destination = objetivo.position;
    }

    void Update()
    {
        
    }
}
