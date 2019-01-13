using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabInfo {

    public GameObject icon;
    public SlotType type;

    public PrefabInfo(GameObject _icon, SlotType _type)
    {
        icon = _icon;
        type = _type;
    }




}
