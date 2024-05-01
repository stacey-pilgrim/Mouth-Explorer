using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    [Header("File Storage")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IData> dataObjects;

    private FileDataHandler dataHandler;

    private Vector3[] startTeethCoords;
    private Quaternion[] startTeethAngles;
    public static DataManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataObjects = new List<IData>(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IData>());

        startTeethCoords = new Vector3[32];
        startTeethAngles = new Quaternion[32];

        GameObject[] dragTeeth = GameObject.FindGameObjectsWithTag("Draggable");
        foreach (GameObject tooth in dragTeeth)
        {
            int number = tooth.GetComponent<ToothUpdate>().toothNumber;
            startTeethCoords[number - 1] = tooth.transform.localPosition;
            startTeethAngles[number - 1] = tooth.transform.localRotation;
        }

        ResetGame();
    }

    public void ResetGame()
    {
        this.gameData = new GameData();

        for (int i = 0; i < 32; i++)
        {
            this.gameData.teethCoords[i] = startTeethCoords[i];
            this.gameData.teethAngles[i] = startTeethAngles[i];
        }

        foreach (IData dataObj in dataObjects)
            dataObj.LoadData(gameData);
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            ResetGame();
        }
        else
        {
            foreach (IData dataObj in dataObjects)
                dataObj.LoadData(gameData);
        }
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
