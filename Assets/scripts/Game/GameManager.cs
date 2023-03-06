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
    public GameObject[] tower;
    public GameObject towerSelector;
    public spawn_torret spawn_TorretScript;
    public Vector3 spawnTourretPos;

    public Vector3 mapaPosition = Vector3.zero;
    public Vector3 buttonPosition = new Vector3(0, 15, 0);

    public bool uIOn;

    #region Platforms
    public NavMeshSurface surfaces;
    public GameObject[] mapSpawn;

    public GameObject button;

    public GameObject[] spawnPoints;
  
    public Dictionary<int, int[]> platformCombination = new Dictionary<int, int[]>()
    {
        {0, new int[] {2,3} },
        {1, new int[] {2} } ,
        {2, new int[] {0,1, 2} } ,
        {3, new int[] {0} }
    };

    public int oldPlataform = 0;
    #endregion

    public int enemiesLeft;
    private int enemiesPerWave = 5;
    public GameObject[] enemies;
    public int enemyWave = 0;

    public bool onSpawn = false;

    public TextMeshProUGUI coinsText;
    public int coins = 30;

    public Button[] turretButton;
    public int[] prices;

    public TextMeshProUGUI towerHealthText;
    public int towerHealth;

    public GameObject postProcesing;

    public float vignetteAnimValue;

    private Volume volume;
    private Vignette vignetteVolume;

    public GameObject dollyCartCam;

    public GameObject pivotCamera;

    public GameObject dollyTrackCam;

    private CinemachineSmoothPath smsp;

    public Vector3 pivotPosition = new Vector3(0, 0, -15);

    public float cameraSpeed = 0.04f;

    public GameObject pause;


    public static GameManager sharedInstance;
    private void Awake()
    {
        Time.timeScale = 1;
        if(sharedInstance == null)
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
        smsp = dollyTrackCam.GetComponent<CinemachineSmoothPath>();

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

        coinsText.text = $"Coins : {coins}";

        volume = FindObjectOfType<Volume>();

    }

    private void Update()
    {
        towerHealthText.text = $"Tower Life : {towerHealth}";

            enemiesLeft = FindObjectsOfType<Enemy_manager>().Length;
       if(enemyWave <= 5)
        {
            if (enemiesLeft <= 0)
            {
            button.SetActive(true);
            }

            //win
        }
        
       if(Input.GetKey(KeyCode.D))
       {
            dollyCartCam.GetComponent<CinemachineDollyCart>().m_Position += cameraSpeed;
            smsp.m_Resolution = 5;
        }

        if(Input.GetKey(KeyCode.A))
       {
            dollyCartCam.GetComponent<CinemachineDollyCart>().m_Position -= cameraSpeed;
            smsp.m_Resolution = 6;

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
        {
            Pause();
        }

    }

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
    public void Exit()
    {
        uIOn = false;
        towerSelector.SetActive(false);
    }
    public void SpawnPlataform()
    {
        int randoPlataform = Random.Range(0, platformCombination[oldPlataform].Length);

        int newPlatform = platformCombination[oldPlataform][randoPlataform];

        Instantiate(mapSpawn[newPlatform], mapaPosition - Vector3.forward * 30, transform.rotation);

        mapaPosition = mapaPosition - Vector3.forward * 30;

        oldPlataform = newPlatform;
    }
    public void MoveButton()
    {
        buttonPosition = buttonPosition - Vector3.forward * 30;
        button.transform.position = buttonPosition;
        pivotCamera.transform.position += pivotPosition;
        smsp.m_Waypoints[2].position += new Vector3 (0,0,-25);
        smsp.m_Waypoints[1].position += new Vector3 (0,0,-25); 
        
    }

    private IEnumerator SpawnEnemyWave(int totalenemies)
    {
        for (int i = 0; i < totalenemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2);
        }
    }

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemies.Length);

        int radomSpawn = Random.Range(0, spawnPoints.Length);

        Instantiate(enemies[randomEnemy], spawnPoints[radomSpawn].transform.position, transform.rotation);

    }

    public void UpdateCoins(int coin)
    {
        coins += coin;

        coinsText.text = $"Coins : {coins}";

    }

    public void BuyTurret(int price)
    {
        coins -= price;
        coinsText.text = $"Coins : {coins}";
    }

    void CanBuy(int button)
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
    
    public void GameOver(bool gameOver = false)
    {
        if(gameOver == true)
        {
            Time.timeScale = 0;
        }
    }

    public void VignetteAnim(float value)
    {
        if (volume.profile.TryGet<Vignette>(out vignetteVolume))
        {
            vignetteVolume.intensity.value = value;
        }
    }

    public void GoToScene(string sceneName)
    {
        //Change to the scene with "Scene Name"
        SceneManager.LoadScene(sceneName);
    }

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

}
