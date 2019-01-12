using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabInfo {

    public GameObject icon;
    public string type;

    public PrefabInfo(GameObject _icon, string _type)
    {
        icon = _icon;
        type = _type;
    }




}
