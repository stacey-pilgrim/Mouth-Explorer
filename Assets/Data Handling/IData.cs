using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public interface IData
{
    void LoadData(GameData data);
    void SaveData(GameData data);
}
