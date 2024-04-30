using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawUpdate : MonoBehaviour, IData
{
    public void LoadData(GameData data)
    {
        transform.rotation = data.mouthAngles;
    }

    public void SaveData(GameData data)
    {
        data.mouthAngles = transform.rotation;
    }
}
