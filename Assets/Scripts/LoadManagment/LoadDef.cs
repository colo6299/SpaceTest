using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDef : MonoBehaviour{

    public GameObject[] keepArray;
    private static GameObject[] keepStatic;
    public static bool kept = false;

    void Start()
    {
        if (!kept)
        {
            keepStatic = keepArray;
            kept = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameObject[] GetKeepArray()
    {
        return keepStatic;
    }

    public static void KeepObjects()
    {
        foreach (GameObject go in keepStatic)
        {
            DontDestroyOnLoad(go);
        }
    }
}
