using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RollInfo {

    public string name;
    public int prefabID;
    public float durability;
    public float[] rollArray;

    public RollInfo(float[] _rollArray)
    {
        name = "";
        prefabID = 0;
        durability = 100;
        rollArray = _rollArray;
    }

}
