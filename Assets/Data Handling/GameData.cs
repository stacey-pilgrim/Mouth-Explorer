using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
//    public List<Vector3> teethCoords;
//    public List<Vector3> teethAngles;

    public Quaternion mouthAngles;

//    public Vector3 playerCoords;
//    public Quaternion playerAngles;

    public GameData()
    {
        mouthAngles = Quaternion.identity;
    }

}
