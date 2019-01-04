using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStrafe : MonoBehaviour {

    public Transform cam;
    public Transform ship;
    public Rigidbody shipR;

    //this was a bad idea, and I regret thinking it
    //public Rigidbody rcam;

    private Vector3 offset;
    private Vector3 slewVec;


    void Start()
    {
        offset = cam.position - ship.position;
        offset = ship.InverseTransformPoint(cam.position);
        slewVec = Vector3.zero;
    }

    void Update()
    {

        //cam.rotation = Quaternion.Slerp(cam.rotation, ship.rotation, 0.95f * Time.deltaTime);

        //float rotx = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.5f, 1.5f);
        //float roty = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1.5f, 1.5f);
        //float rotz = 0;//Mathf.Clamp(-Input.GetAxis("Horizontal"), -1, 1);

        Vector3 rotVec = shipR.angularVelocity;

        slewVec *= 59 / 60;
        slewVec.x += rotVec.y/59;
        slewVec.y += rotVec.x/59;
        

        cam.position = ship.TransformPoint(offset + slewVec);

        //rcam.angularVelocity = cam.transform.TransformDirection(rotVec) * Time.deltaTime * 10 + rcam.angularVelocity;
        //rcam.maxAngularVelocity = 5f;
    }


}
