using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class OptionManager : MonoBehaviour
{
    #region Variables

    #region Audio
    private AudioSource musicManager;
    public AudioClip menuMusic;

    private AudioSource effectsManager;
    public AudioClip effectsMusic;
    #endregion

    #region Data persistence
    public Slider volumSliderMusic; //Float value
    public Slider volumSliderEfects; //Float value

    private float musicValue;//Slider float value
    private float effectsValue;//Slider float value
    #endregion

    #endregion

    #region Methots
    private void Start()
    {
        //Get the variable with component
        musicManager = GetComponent<AudioSource>();
        musicManager.PlayOneShot(menuMusic);

        effectsManager = GameObject.Find("Effects").GetComponent<AudioSource>();

        //To download the data persitence data
        LoadUserOptions();

        //To save it in the data persistence
        SaveUserOptions();
    }
    #endregion 

    #region Functions

    public void SaveUserOptions() //Save the values in the data persistence
    {
        //persistencia de datos entre escenas

        DataPersistence.SharedInfo.musicGameDP = musicValue;
        DataPersistence.SharedInfo.effectsGameDP = effectsValue;

        //Data persistence between scenes
        DataPersistence.SharedInfo.SaveForFutureGames();
    }

    public void LoadUserOptions()
    {
        //If there is kwy it works

        musicValue = DataPersistence.SharedInfo.musicGameDP;//Take the last value of the slider

        effectsValue = DataPersistence.SharedInfo.effectsGameDP;//Take the last value of the slider

        LoadVolume();
    }

    #region UpdateVolume
    public void LoadVolume() //Set the vaule of the data in the slider
    {
        volumSliderMusic.GetComponent<Slider>().value = musicValue;
        volumSliderEfects.GetComponent<Slider>().value = effectsValue;
    }

    //parTo save the current changes of the slider in the data pesistence
    public void VolumeSelection(float V) 
    {
        musicValue = V;
        musicManager.volume = musicValue;
    }
    public void VolumeSelection2(float V) 
    {
        effectsValue = V;
        effectsManager.volume = effectsValue;
    }
    #endregion

    //Try the effect to listen if the volume is ok
    public void TryEffects()
    {
        effectsManager.PlayOneShot(effectsMusic);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion
   
}
