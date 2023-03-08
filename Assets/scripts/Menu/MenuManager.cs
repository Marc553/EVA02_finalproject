using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private AudioSource musicaManager;
    public AudioClip menuMusic;

    private void Start()
    {
        musicaManager = GetComponent<AudioSource>();
        musicaManager.PlayOneShot(menuMusic);

        musicaManager.volume = DataPersistence.SharedInfo.musicGameDP;
    }

    public void GoToScene(string sceneName)
    {
        //Change to the scene with "Scene Name"
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;//quitar antes d build

#endif
        Application.Quit();
    }
}
