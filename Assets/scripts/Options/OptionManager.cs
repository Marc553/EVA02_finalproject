using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class OptionManager : MonoBehaviour
{
    #region VARAIBLES
    //variables globales
    private AudioSource musicManager;
    public AudioClip menuMusic;

    private AudioSource effectsManager;
    public AudioClip effectsMusic;

    //datapersitence
    public Slider volumSliderMusic; //donde sacaremos el valor (float)
    public Slider volumSliderEfects; //donde sacaremos el valor (float)

    private float musicValue;//valor del slider (float)
    private float effectsValue;//valor del slider (float)
    #endregion

    private void Start()
    {
        musicManager = GetComponent<AudioSource>();
        musicManager.PlayOneShot(menuMusic);

        effectsManager = GameObject.Find("Effects").GetComponent<AudioSource>();

        LoadUserOptions();
        SaveUserOptions();
    }

    #region FUNCIONES

    public void SaveUserOptions() //cuando se ejecuta guarda en el data persitance las variables en su respectiva caja
    {
        //persistencia de datos entre escenas

        DataPersistence.SharedInfo.musicGameDP = musicValue;
        DataPersistence.SharedInfo.effectsGameDP = effectsValue;

        //persistencia de datos entre partidas
        DataPersistence.SharedInfo.SaveForFutureGames();
    }

    public void LoadUserOptions()
    {
        //si tiene esta clave, entonces tiene todas

        musicValue = DataPersistence.SharedInfo.musicGameDP;//coge el último valor que ha tenido el slider

        effectsValue = DataPersistence.SharedInfo.effectsGameDP;//coge el último valor que ha tenido el slider

        LoadVolume();
    }

    #region UpdateVolume
    public void LoadVolume() //metemos el valor de numeroVolumen en el slider del menú de opciones, para cargar el volumen que se tenia configurado en otras partidas
    {
        volumSliderMusic.GetComponent<Slider>().value = musicValue;
        volumSliderEfects.GetComponent<Slider>().value = effectsValue;
    }
    public void VolumeSelection(float V) //para que cuando cambiemos el valor del slider guardemos el valor para pasarlo al datapersistance
    {
        musicValue = V;
        musicManager.volume = musicValue;
    }
    public void VolumeSelection2(float V) //para que cuando cambiemos el valor del slider guardemos el valor para pasarlo al datapersistance
    {
        effectsValue = V;
        effectsManager.volume = effectsValue;
    }
    #endregion

    public void TryEffects()
    {
        Debug.Log("si");
        effectsManager.PlayOneShot(effectsMusic);
    }

    //para llegar a la menu scene
    public void GoToScene(string sceneName)
    {
        // Cargamos la escena que tenga por nombre sceneName
        SceneManager.LoadScene(sceneName);
    }

    #endregion
   
}
