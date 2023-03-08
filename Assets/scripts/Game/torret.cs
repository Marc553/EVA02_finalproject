using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torret : MonoBehaviour
{
    #region Variable

    #region Look For Enemy
    public GameObject target;//The enemy that is following
    
    public float range = 15f;//The tourret rang

    public string enemyTag = "Enemy";//The targets will follow

    public Transform partToRotate;//Element will rotate

    public float turnSpeed = 10f;//Rotation speed
    #endregion

    #region Fire
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    public GameObject bulletPrefab;//Bullet
    public Transform firePoint;//Where do you shoot from
    #endregion

    #region Audio
    private AudioSource tourretManager;
    public AudioClip fire;
    #endregion

    #endregion

    #region Methots
    void Start()
    {
        //Get all varaibles with their components
        tourretManager = GetComponent<AudioSource>();

        tourretManager.volume = DataPersistence.SharedInfo.effectsGameDP;

        //Start the serch of the tourret
        InvokeRepeating("UpdateTarget", 0f, 1.5f);

    }

    //Enemy search 
    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);//Find all the enemies in the scene
        float shortestDistance = Mathf.Infinity;//Create a vairable where will calculate the shortestDistance
        GameObject nearestEnemy = null;//Will save the nearestEnemy

        //Select an "enemy" in the "enemies" array (game objects)
        foreach (GameObject enemy in enemies)
        {
            //Calculate the distance to that supousted enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //If the distance to enemy is less than the shortest distance, the nearest enemy is that enemy 
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //If the closest enemy is someone and the distance to that someone is less than the range, the new target will be the enemy.
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
        }
    }

    void Update()
    { 
        //If there isn't any target, nothing happens
        if (target == null)
            return;
    
    //Target look on
        Vector3 dir = target.transform.position - transform.position;//New vector 3 taht saves the bullet diretion
        Quaternion LookRotation = Quaternion.LookRotation(dir);//Look to that target
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, LookRotation, Time.deltaTime * turnSpeed).eulerAngles ;//Rotates to that look
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);//Says to the rotation part to rotate      
        
        //Fire count down
        if(fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    #endregion

#region Function

    void Shoot()
    {
       GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        tourretManager.PlayOneShot(fire);

        if (bullet != null)
            bullet.Seek(target);
        
    }
    

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); 
    }
    #endregion 
}
