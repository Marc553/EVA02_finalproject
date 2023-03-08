using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InfoManager : MonoBehaviour
{
    private AudioSource musicaManager;
    public AudioClip infoMusic;

    private void Start()
    {
        musicaManager = GetComponent<AudioSource>();
        musicaManager.PlayOneShot(infoMusic);

        musicaManager.volume = DataPersistence.SharedInfo.musicGameDP;
    }


    public void GoToScene(string sceneName)
    {
        //Change to the scene with "Scene Name"
        SceneManager.LoadScene(sceneName);
    }

}
