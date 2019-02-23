using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Ship : Entity
{
    public Rigidbody physics;

    public float Acceleration = 10f;
    public float MaxSpeed = 20f;
    public float SkidRatio = 0.8f;

    private float xavg;
    private float yavg;
    private float zavg;

    public override void Start()
    {
        base.Start();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void Update()
    {
        base.Update();

        AngleShip();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        FlightSkid();
    }

    void FlightSkid()
    {

        //clamp velocity
        physics.velocity = Vector3.ClampMagnitude(physics.velocity, MaxSpeed);

        if (!Input.GetKey(KeyCode.LeftShift))
        {

            //do the skid thing
            Vector3 calcVel = physics.velocity;
            float speed = calcVel.magnitude;
            calcVel = Vector3.Slerp(calcVel.normalized, physics.transform.forward, SkidRatio * Time.deltaTime);
            calcVel *= speed;
            physics.velocity = calcVel;

            //do the thrust thing
            physics.velocity += physics.transform.forward * Time.deltaTime * Acceleration;
        }
    }

    void AngleShip()
    {
        Vector3 euler = Vector3.zero;

        //Debug.Log(Input.GetAxis("Mouse Y"));

        euler.x = -Input.GetAxis("Mouse Y") + euler.x;
        euler.y = Input.GetAxis("Mouse X") + euler.y;

        float rotx = Mathf.Clamp(-Input.GetAxis("Mouse Y"), -1.5f, 1.5f);
        float roty = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.5f, 1.5f);
        float rotz = Mathf.Clamp(-Input.GetAxis("Horizontal"), -1, 1);

        Vector3 rotVec = new Vector3(rotx, roty, rotz);

        physics.angularVelocity = physics.transform.TransformDirection(rotVec) * Time.deltaTime * 10 + physics.angularVelocity;

        physics.maxAngularVelocity = 5f;


        //transform.RotateAround(ship.transform.right, rotx  * Time.deltaTime * 2);
        //transform.RotateAround(ship.transform.up, roty  * Time.deltaTime * 2);
        //transform.RotateAround(ship.transform.forward, rotz  * Time.deltaTime * 3.5f);
    }
}
