using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private void Awake()
    {
        setUpSingleton();
    }

    public void setUpSingleton()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            Load();
        }
    }

    public SaveData currentData;

    //public string sceneToNotSave;
    private string saveName = "saveData";
    public bool dontSave;

    public void Save()
    {
#if UNITY_EDITOR
        if (dontSave)
        {
            return;
        }
#endif
        Debug.Log("Saving...");
        string destination = Application.persistentDataPath + "/" + saveName + ".save";

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(destination, FileMode.Create);
        serializer.Serialize(stream, currentData);
        stream.Close();
    }

    public void Load()
    {
        string destination = Application.persistentDataPath + "/" + saveName + ".save";
        if (File.Exists(destination))
        {
            Debug.Log("Loading...");
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(destination, FileMode.Open);
            currentData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("No save file found");
            currentData.health = 30;
            currentData.currentLevelIndex = 0;
            //currentData = new SaveData();
        }
    }

    public void Delete()
    {
        string destination = Application.persistentDataPath + "/" + saveName + ".save";
        if (File.Exists(destination))
        {
            File.Delete(destination);
        }
        else
        {
            Debug.LogWarning("No save file found");
        }
    }

    public void DestroySaveManager()
    {
        instance = null;
        Destroy(gameObject);
    }
}

