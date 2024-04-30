using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IData> dataObjects;

    private FileDataHandler dataHandler;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataObjects = new List<IData>(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IData>());
    }

    public void ResetGame()
    {
        this.gameData = new GameData();

        foreach (IData dataObj in dataObjects)
            dataObj.LoadData(gameData);
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
            this.gameData = new GameData();

        foreach (IData dataObj in dataObjects) 
            dataObj.LoadData(gameData);
    }

    public void SaveGame()
    {
        foreach (IData dataObj in dataObjects)
        {
            dataObj.SaveData(gameData);
        }

        dataHandler.Save(gameData);
    }
}
