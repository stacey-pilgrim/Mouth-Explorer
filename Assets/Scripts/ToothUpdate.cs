using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothUpdate : MonoBehaviour, IData
{
    public int toothNumber;
    public void LoadData(GameData data)
    {
        transform.localPosition = data.teethCoords[toothNumber - 1];
        transform.localRotation = data.teethAngles[toothNumber - 1];
    }

    public void SaveData(GameData data)
    {
        data.teethCoords[toothNumber - 1] = transform.localPosition;
        data.teethAngles[toothNumber - 1] = transform.localRotation;
    }
}
