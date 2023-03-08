using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    #region Variables
    private AudioSource musicaManager;
    public AudioClip menuMusic;
    #endregion

    #region Methot
    private void Start()
    {
        //Get the variables with the components
        musicaManager = GetComponent<AudioSource>();
        musicaManager.PlayOneShot(menuMusic);

        musicaManager.volume = DataPersistence.SharedInfo.musicGameDP;
    }
    #endregion

    #region Functions
    public void GoToScene(string sceneName)
    {
        //Change to the scene with "Scene Name"
        SceneManager.LoadScene(sceneName);
    }

    //Exit the game
    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}
