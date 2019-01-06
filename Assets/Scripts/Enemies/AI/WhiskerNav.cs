using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskerNav : MonoBehaviour {

    public Transform eye;

    public float center;
    public float topCenter;
    public float topRight;
    public float right;
    public float bottomRight;
    public float bottom;
    public float bottomLeft;
    public float left;
    public float topLeft;
    public float lastDistance;

    private float checkTime;

    private Vector3 vC;
    private Vector3 vTC;
    private Vector3 vTR;
    private Vector3 vR;
    private Vector3 vBR;
    private Vector3 vB;
    private Vector3 vBL;
    private Vector3 vL;
    private Vector3 vTL;


    void Start()
    {
        vC = new Vector3(0, 0, 0);
        vTC = new Vector3(0, 1, 0);
        vTR = new Vector3(1, 1, 0);
        vR = new Vector3(1, 0, 0);
        vBR = new Vector3(1, -1, 0);
        vB = new Vector3(0, -1, 0);
        vBL = new Vector3(-1, -1, 0);
        vL = new Vector3(-1, 0, 0);
        vTL = new Vector3(-1, 1, 0);
    }


    void Update()
    {
        
    }


    public void UpdateWhiskers(float distance)
    {
        vC.z = distance;
        vTC.z = distance;
        vTR.z = distance;
        vR.z = distance;
        vBR.z = distance;
        vB.z = distance;
        vBL.z = distance;
        vL.z = distance;
        vTL.z = distance;

        center = CheckWhisker(vC);
        topCenter = CheckWhisker(vTC);
        topRight = CheckWhisker(vTR);
        right = CheckWhisker(vR);
        bottomRight = CheckWhisker(vBR);
        bottom = CheckWhisker(vB);
        bottomLeft = CheckWhisker(vBL);
        left = CheckWhisker(vL);
        topLeft = CheckWhisker(vTL);

        lastDistance = distance;
    }


    private float CheckWhisker(Vector3 whisker3)
    {
        float d = 0;
        RaycastHit whisker;
        //Physics.Raycast(eye.position, transform.InverseTransformDirection(whisker3), out whisker);

        if (Physics.SphereCast(eye.position, 5, transform.InverseTransformDirection(whisker3), out whisker))
        {
            d = whisker.distance;
        }

        return d;
    }   

}
