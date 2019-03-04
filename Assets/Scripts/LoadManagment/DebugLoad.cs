using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLoad : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            LoadDef.KeepObjects();
            SceneManager.LoadSceneAsync("MapTest");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            LoadDef.KeepObjects();
            SceneManager.LoadSceneAsync("AsteroidScene");
        }
    }
}
