using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence SharedInfo;

    //Data to keep between scenes
    public int musicGameDP;

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

    public void SaveForFutureGames()
    {
        //Int skin

        PlayerPrefs.SetInt("MUSICGAME", musicGameDP);
    }

    public void OnApplicationQuit()
    {
        SaveForFutureGames();
    }
}
