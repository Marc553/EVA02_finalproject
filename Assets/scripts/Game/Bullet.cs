using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Vairables
    private GameObject target;

    public float speed = 70f;

    public int damage;

    public GameObject impactEffect; 
    public GameObject impactEffect2; 

    public void Seek(GameObject _target)
    {
        target = _target;
    }

    private AudioSource bulletManager;
    public AudioClip fire;
    public AudioClip enemy;
    #endregion  

    private void Start()
    {
        bulletManager = GetComponent<AudioSource>();
        bulletManager.volume = DataPersistence.SharedInfo.effectsGameDP;
        bulletManager.PlayOneShot(fire);


    }
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        void HitTarget()
        {
            bulletManager.PlayOneShot(enemy);
            GameObject effect1Ins = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            GameObject effect2Ins = (GameObject)Instantiate(impactEffect2, transform.position, transform.rotation);
            Destroy(effect1Ins, 1f);
            Destroy(effect2Ins, 1f);
            target.GetComponent<Enemy_manager>().health -= damage;
                Destroy(gameObject);
        }


    }
}
