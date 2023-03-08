using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Vairables
    private GameObject target;//Who will follow

    public float speed = 70f;//Speed of the bullet

    public int damage;//Damage that do to the enemies

    public GameObject impactEffect;//Explosion effect
    public GameObject impactEffect2;//Enemy hit effect

    public void Seek(GameObject _target)//That will serch the gameobjecto to follow
    {
        target = _target;
    }

    private AudioSource bulletManager;
    public AudioClip fire;
    public AudioClip enemy;
    #endregion

    #region Metohts
    private void Start()
    {
        //Get the variable with his component
        bulletManager = GetComponent<AudioSource>();
        bulletManager.volume = DataPersistence.SharedInfo.effectsGameDP;
        bulletManager.PlayOneShot(fire);
    }
    void Update()
    {
        //If there isn't a target will destroy the bullet
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        //Calculates the distance between the bullet and the enemy
        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;//Follow the enemy

        //If the bullet arrives to the enemy it will HITTARGET
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        //What the bullet do when hit
        void HitTarget()
        {
            bulletManager.PlayOneShot(enemy);//Play enemy hit
            GameObject effect1Ins = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);//Particle hit
            GameObject effect2Ins = (GameObject)Instantiate(impactEffect2, transform.position, transform.rotation);//Particle hit
            //destroy particles
            Destroy(effect1Ins, 1f);
            Destroy(effect2Ins, 1f);
            //Subtracts damage from the enemy's life
            target.GetComponent<Enemy_manager>().health -= damage;
                Destroy(gameObject);
        }
    }
    #endregion
}
