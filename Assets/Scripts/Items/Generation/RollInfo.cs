using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RollInfo {

    public string name;
    public SlotType slot;
    public Rarity rarity;
    public int prefabID;
    public float durability;
    public float[] rollArray;

    public RollInfo(float[] _rollArray)
    {
        name = "";
        slot = SlotType.None;
        rarity = Rarity.None;
        prefabID = 0;
        durability = 100;
        rollArray = _rollArray;
    }

}
