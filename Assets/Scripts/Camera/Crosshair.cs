using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {


    public Transform ship;
    public Camera cam;
    public Transform crosshair;

    void Update()
    {
        /*
        Vector3 point = ship.forward * 10000;
        point = cam.WorldToScreenPoint(point);
        Debug.Log(point);
        point.z = 2;
        point = cam.ScreenToWorldPoint(point);
        crosshair.position = point;
        crosshair.rotation = ship.rotation;
        */

        Vector3 direct = ship.forward - ship.position;



    }





}
