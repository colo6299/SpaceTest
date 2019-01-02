using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStrafe : MonoBehaviour {

    public Transform cam;
    public Transform ship;

    private Vector3 offset;


    void Start()
    {
        offset = cam.position - ship.position;
    }

    void Update()
    {
        cam.position = ship.position + offset;
    }


}
