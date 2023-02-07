using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Swpan_area : MonoBehaviour
{
    public NavMeshSurface surfaces;
    public GameObject[] mapSpawn;

    public GameObject button;


    public Dictionary<int, int[]> platformCombination = new Dictionary<int, int[]>()
    {
        {0, new int[] {2,3} },
        {1, new int[] {2} } ,
        {2, new int[] {0,1, 2} } ,
        {3, new int[] {0} }
    };

    public int oldPlataform = 0;


    // Use this for initialization
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            surfaces.BuildNavMesh();
        }
    }

    public void SpawnPlataform()
    {
        int randoPlataform = Random.Range(0, platformCombination[oldPlataform].Length);

        int newPlatform = platformCombination[oldPlataform][randoPlataform];

         Instantiate(mapSpawn[newPlatform], GameManager.sharedInstance.mapaPosition - Vector3.forward * 30, GameManager.sharedInstance.transform.rotation);

        GameManager.sharedInstance.mapaPosition = GameManager.sharedInstance.mapaPosition - Vector3.forward * 30;

        oldPlataform = newPlatform;
    }

    public void MoveButton()
    {
        GameManager.sharedInstance.buttonPosition = GameManager.sharedInstance.buttonPosition - Vector3.forward * 30;
        transform.position = GameManager.sharedInstance.buttonPosition;
    }
    

    private void OnMouseDown()
    { 
            MoveButton();
            SpawnPlataform();
            surfaces.BuildNavMesh();
        
    }

}

