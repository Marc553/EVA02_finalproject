using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Swpan_area : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    

    // Use this for initialization
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < surfaces.Length; i++)
                {
                surfaces[i].BuildNavMesh();

                }
            
        }
    }

}

