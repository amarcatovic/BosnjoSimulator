using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveLogic : MonoBehaviour
{
    private string path;
    public static GameSaveLogic si;
    private void Awake()
    {
        if (FindObjectsOfType<GameSaveLogic>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            path = Application.persistentDataPath + "/saves/";
        }
    }
    public void Save()
    {
        Directory.CreateDirectory(path);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path + "bosnjo_sg.dat", FileMode.Create);
        SaveData sd = new SaveData();

        sd = FindObjectOfType<Gameplay>().GetSaveData();

        bf.Serialize(file, sd);
        file.Close();
    }

    // returns true if load file exists
    public void Load()
    {
       BinaryFormatter bf = new BinaryFormatter();
       FileStream file = File.Open(path + "bosnjo_sg.dat", FileMode.Open);
       SaveData sd = (SaveData)bf.Deserialize(file);
       file.Close();

        FindObjectOfType<Gameplay>().SetSaveData(sd);
    }

    public void DeleteSave()
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
        Directory.CreateDirectory(path);
    }

    public bool SaveGameExists()
    {
        if (File.Exists(path + "bosnjo_sg.dat"))
            return true;

        return false;
    }
}