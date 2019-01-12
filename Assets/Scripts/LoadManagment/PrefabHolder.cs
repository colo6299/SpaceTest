﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour {


    /// <summary>
    /// Misc. prefabs 
    /// </summary>
    public GameObject[] prefabArray;

    public GameObject[] itemArray;
    public static GameObject[] staticItemArray;
    public static int itemLength;

    public PrefabInfo[] iconArray;
    public static PrefabInfo[] staticIconArray;

    public GameObject[] bossArray;

    void Start()
    {
        itemLength = itemArray.Length;
        staticItemArray = itemArray;
        staticIconArray = iconArray;
    }
}
