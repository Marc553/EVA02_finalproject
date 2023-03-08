using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    #region Variables
    #region Tower Spawn
    public GameObject[] tower;//Tourrets array
    public GameObject towerSelector;//Panel with the tourret selector 
    public spawn_torret spawn_TourretScript; 
    public Vector3 spawnTourretPos;//Save the position of the new tourret
    #endregion

    public Vector3 mapaPosition = Vector3.zero;//Start vector to calcule the new position
    public Vector3 buttonPosition = new Vector3(0, 15, 0);//Vector to plus 

    public bool uIOn;//checker of the IU

    #region Platform
    public NavMeshSurface surfaces;//Creation of the Navmesh
    public GameObject[] mapSpawn;//Platforms array

    public GameObject button;//Button to generate the new Navmesh terrain

    public GameObject[] spawnPoints;//Spawn points to enemies 
  
    public Dictionary<int, int[]> platformCombination = new Dictionary<int, int[]>()//Check which platform is compatible with the other
    {
        {0, new int[] {2,3} },//The platform 0 is compatible with 2 and 3
        {1, new int[] {2} } ,//The platform 1 is compatible with 2
        {2, new int[] {0,1, 2} } ,//The platform 2 is compatible with 0, 1 and 2
        {3, new int[] {0} }//The platform 3 is compatible with 0
    };

    public int oldPlataform = 0;//Save old platform to use dictionary
    #endregion

    #region Enemy Spawn
    public int enemiesLeft;
    private int enemiesPerWave = 5;//Number of enemies per wave
    public GameObject[] enemies;//Enemies array
    public int enemyWave = 0;//Count the number of waves of enemies that have passed
    public bool onSpawn = false;//Check to start the enemy spawn
    #endregion

    #region UI Sistem
    public TextMeshProUGUI coinsText;//Shows the coins number
    public int coins = 30;//Coins

    public Button[] turretButton;//Array with all tourrets
    public int[] prices;//The prices 

    public TextMeshProUGUI towerHealthText;//Shows the tower health
    public int towerHealth;

    public GameObject win;
    public GameObject lose;

    #endregion

    #region Postprocesing
    public GameObject postProcesing;

    public float vignetteAnimValue;//Value to the on the postprocesig

    private Volume volume;//Volume component 
    private Vignette vignetteVolume;//Vignette effect 
    #endregion

    #region Camera Controll
    public GameObject dollyCartCam;//Moves the cam 

    public GameObject pivotCamera;//Where the cam look

    public GameObject dollyTrackCam;//Cam way

    private CinemachineSmoothPath cinemachineSmoothPathCam;//Component

    public Vector3 pivotPosition = new Vector3(0, 0, -15);//Position to plus to the pivot

    public float cameraSpeed = 0.04f;//Cameera rotation speed
    #endregion

    //Pause
    public GameObject pause;//Pause panel

    //Music Manager
    private AudioSource musicaManager;
    public AudioClip gameMusic;
    

    public static GameManager sharedInstance;
    #endregion

    #region Methots
    //Start the game
    private void Awake()
    {
        Time.timeScale = 1;
        //Spawn a game manager if there isn't one
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else 
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
        #region Get components
        //Get all varaibles with their components
        cinemachineSmoothPathCam = dollyTrackCam.GetComponent<CinemachineSmoothPath>();

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

        volume = FindObjectOfType<Volume>();

        musicaManager = GetComponent<AudioSource>();
        #endregion

        coinsText.text = $"Coins : {coins}";//Shows the money to the player

        musicaManager.volume = DataPersistence.SharedInfo.musicGameDP;//Gets the music volume 
        musicaManager.PlayOneShot(gameMusic);//Start music

    }

    private void Update()
    {
            //Update the tower life to defend 
            towerHealthText.text = $"Tower Life : {towerHealth}";

        //Check all the enemies at the scene
        enemiesLeft = FindObjectsOfType<Enemy_manager>().Length;

        //Limits the game with 5 rounds
        if (enemyWave <= 5)
        {
                
                //Checks when there isn't anymore enemies to open the Platform button

                if (enemiesLeft <= 0)
                {
                    button.SetActive(true);//Active the Platform button
                }

                if (enemiesLeft <= 0 && enemyWave == 5)
                {
                    win.SetActive(true);//Active the Platform button
                }
                
        }

            if (Input.GetKey(KeyCode.D))
            {
            dollyCartCam.GetComponent<CinemachineDollyCart>().m_Position -= cameraSpeed;
            cinemachineSmoothPathCam.m_Resolution = 5;
            }

        if(Input.GetKey(KeyCode.A))
        {
            dollyCartCam.GetComponent<CinemachineDollyCart>().m_Position += cameraSpeed;
            cinemachineSmoothPathCam.m_Resolution = 6;

        }

        if (onSpawn == true)
        {
            enemyWave++;
            button.SetActive(false);
            enemiesPerWave += 5;
            StartCoroutine(SpawnEnemyWave(enemiesPerWave));
            onSpawn = false;
        }

        CanBuy(0);
        CanBuy(1);
        CanBuy(2);

        VignetteAnim(vignetteAnimValue);

        if(Input.GetKeyDown(KeyCode.Space))
            Pause();

            
        }
    #endregion

    #region Functions
    #region Spawn Tourret
    //Functions to spawn the every torruet
    public void SpawnTower1()
    {
        Instantiate(tower[0], spawnTourretPos, Quaternion.identity);
    }
    public void SpawnTower2()
    {
        Instantiate(tower[1], spawnTourretPos, Quaternion.identity);
    }
    public void SpawnTower3()
    {
        Instantiate(tower[2], spawnTourretPos, Quaternion.identity);
    }
    #endregion

    #region Spawn Platforms
    //Function that spawn the new platform
    public void SpawnPlataform()
    {
        int randoPlataform = Random.Range(0, platformCombination[oldPlataform].Length);//Take a random platform taking into account the dictionary combinations

        int newPlatform = platformCombination[oldPlataform][randoPlataform];//Saves the new paltform in a variable

        Instantiate(mapSpawn[newPlatform], mapaPosition - Vector3.forward * 30, transform.rotation);//Spawns the new platform in the new position

        mapaPosition = mapaPosition - Vector3.forward * 30;//Each time calculates the new position

        oldPlataform = newPlatform;//Save the new platform like the old to restart the cycle
    }
    #endregion

    #region Move button
    public void MoveButton()
    {
        buttonPosition = buttonPosition - Vector3.forward * 30;//Calcualtes the button spawn
        button.transform.position = buttonPosition;//Updates the button spawn
        pivotCamera.transform.position += pivotPosition;//Updates the pivot point
        //Move the way point of the cam
        cinemachineSmoothPathCam.m_Waypoints[2].position += new Vector3 (0,0,-25);
        cinemachineSmoothPathCam.m_Waypoints[1].position += new Vector3 (0,0,-25); 
    }
    #endregion

    #region Enemies spawn
    
    //Spawn enemies by waves
    private IEnumerator SpawnEnemyWave(int totalenemies)
    {
        for (int i = 0; i < totalenemies; i++)
        {
            SpawnEnemy();//Actives the enemy spawn 
            yield return new WaitForSeconds(1);//Time between every spawn 
        }
    }

    //Spawn enemy 
    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);//Random enemy

        int radomSpawn = Random.Range(0, spawnPoints.Length);//Random positon 

        Instantiate(enemies[randomEnemy], spawnPoints[radomSpawn].transform.position, transform.rotation);//Spawn enemy

    }
    #endregion

    #region Buy System
    //Update the coins when an enemy dies
    public void UpdateCoins(int coin)
    {
        coins += coin;

        coinsText.text = $"Coins : {coins}";

    }

    //Buy a turret by subtracting the price
    public void BuyTurret(int price)
    {
        coins -= price;//Subtract the price of the tourret to the money
        coinsText.text = $"Coins : {coins}";
        towerSelector.SetActive(false);
        uIOn = false;
    }

    //Check if the money is enough to buy
    void CanBuy(int button)//Turret button number and its assigned price 
    {
        if (coins < prices[button])
        {
            turretButton[button].interactable = false;
        }
        else
        {
            turretButton[button].interactable = true;
        }
    }
    #endregion

    #region Postprocesing
    //increase the intensity moderately
    public void VignetteAnim(float value)
    {
        if (volume.profile.TryGet<Vignette>(out vignetteVolume))
        {
            vignetteVolume.intensity.value = value;
        }
    }
    #endregion 

     //Change the scene
    public void GoToScene(string sceneName)
    {
        //Change to the scene with "Scene Name"
        SceneManager.LoadScene(sceneName);
    }

    #region Pause
    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    #endregion 

    //Stop the game
    public void GameOver(bool gameOver = false)
    {
        if (gameOver == true)
        {
            lose.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    //Exit the tourret menu
    public void ExitUI()
    {
        uIOn = false;
        towerSelector.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    #endregion 
}
