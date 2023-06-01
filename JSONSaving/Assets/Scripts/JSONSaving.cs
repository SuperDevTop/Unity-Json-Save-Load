using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JSONSaving : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public GameObject mainUI;
    
    private PlayerData playerData;

    private string path = "";
    private string persistentPath = "";

    void Start()
    {
        CreatePlayerData();
        SetPaths();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }

        mainUI.transform.localScale = new Vector3(Screen.width / 1366f, Screen.height / 768f, 1f);
    }

    private void CreatePlayerData()
    {
        playerData = new PlayerData("Nico", 200f, 10f, 3);
    }  
    
    private void SetPaths()
    {
        //path = Application.dataPath + "/StreamingAssets/SaveData.json";
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    public void SaveData()
    {
        string savePath = path;

        Debug.Log("Saving Data at " + savePath);
        text1.text = "Saving Data at " + savePath;

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);
        text2.text = json.ToString();

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
        text1.text = "";
        text2.text = data.ToString();
    }
}
