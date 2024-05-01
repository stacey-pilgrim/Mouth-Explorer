using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothUpdate : MonoBehaviour, IData
{
    public int toothNumber;
    public void LoadData(GameData data)
    {
        transform.position = data.teethCoords[toothNumber - 1];
        transform.rotation = data.teethAngles[toothNumber - 1];
    }

    public void SaveData(GameData data)
    {
        data.teethCoords[toothNumber - 1] = transform.position;
        data.teethAngles[toothNumber - 1] = transform.rotation;
    }
}
