using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Quaternion mouthAngles;

    public Vector3 playerCoords;
    public Quaternion playerAngles;
    public Quaternion cameraAngles;

    public Vector3[] teethCoords;
    public Quaternion[] teethAngles;

    public GameData()
    {
        this.mouthAngles = Quaternion.identity;
        this.playerCoords = new Vector3(0, -0.12f, -2.5f);
        this.playerAngles = Quaternion.identity;
        this.cameraAngles = Quaternion.identity;
        this.teethCoords = new Vector3[32];
        this.teethAngles = new Quaternion[32];
    }

}
