using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    int money=0;
    public int Money
    {
        get { return money; }
        set { money = (int)Math.Clamp( value,0,999999999999);
            onChangeMoney?.Invoke();
            SavePlayerData();
        }
    }

    private void Start()
    {
        LoadPlayerGameData();
    }


    void SavePlayerData()
    {
        SaveData saveData = new();
        saveData.Money = Money;


        string json = JsonUtility.ToJson(saveData);

        string path = $"{Application.dataPath}/Save/";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);

    }

    void LoadPlayerGameData()
    {
        string path = $"{Application.dataPath}/Save/";
        string fullPath = $"{path}Save.json";

        if (Directory.Exists(path) && File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            Money = saveData.Money;
        }else
        {
            Money = 10000;
        }
    }


    public System.Action onChangeMoney { get; set; }
}
