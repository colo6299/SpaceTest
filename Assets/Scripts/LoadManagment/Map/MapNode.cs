using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNode : MonoBehaviour {

    //scene associated with this node
    public string sceneName;

    public bool active;
    public bool occupied;
    public GameObject[] connected;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

}
