using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour {


    /// <summary>
    /// Misc. prefabs 
    /// </summary>
    public GameObject[] prefabArray;

    public GameObject[] itemArray;
    public static GameObject[] staticItemArray;

    public GameObject[] TierArray;
    public static GameObject[] staticTierArray;

    public PrefabInfo[] iconArray;
    public static PrefabInfo[] staticIconArray;

    public GameObject[] bossArray;

    void Start()
    {
        staticItemArray = itemArray;
        staticIconArray = iconArray;
        staticTierArray = TierArray;
    }
}
