using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    #region Variables
    //Conect between scenes
    public static DataPersistence SharedInfo;

    //Data to keep between scenes
    public float musicGameDP;
    public float effectsGameDP;

    #endregion

    #region Methots
    //To have only one instace
    private void Awake()
    {

        //If the instance do not exist
        if (SharedInfo == null)
        {
            //Configure the instance
            SharedInfo = this;
            //We check to don't be destroyed by the change of scenes
            DontDestroyOnLoad(SharedInfo);
        }
        else
        {
            //If there is already one, wue destroy it
            Destroy(this);
        }
    }
    private void Start()
    {
        //If there is a "MUSIC" the variables will be saved
        if (PlayerPrefs.HasKey("MUSICA"))
        {
            musicGameDP = PlayerPrefs.GetFloat("MUSIC");//Float variable to volume music
            effectsGameDP = PlayerPrefs.GetFloat("EFFECT");//Float variable to volume effets

        }
        else
        { //The defect value
            musicGameDP = 0.75f;
            effectsGameDP = 0.75f;

        }
    }
    #endregion

    #region Functions
    public void SaveForFutureGames()
    {
        //Float volume

        PlayerPrefs.SetFloat("MUSIC", musicGameDP);
        PlayerPrefs.SetFloat("EFFECT", effectsGameDP);
    }

    public void OnApplicationQuit()
    {
        SaveForFutureGames(); 
    }
    #endregion 
}
