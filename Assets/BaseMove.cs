using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour {

    public float acceleration = 10;

    private bool slowing = false;
    private bool boosting = false;

    private Vector3 boostLock;

    public Rigidbody ship;



	void Update ()
    {

        ship.velocity += transform.TransformDirection(new Vector3(0, 0, acceleration * Time.deltaTime * Input.GetAxis("Vertical")));

        if (!slowing & Input.GetKey(KeyCode.LeftShift))
        {
            boostLock = transform.InverseTransformDirection(ship.velocity);
            boostLock.z = 0;
            boostLock = boostLock.normalized;
        }
        slowing = Input.GetKey(KeyCode.LeftShift);


        Vector3 euler = Vector3.zero;
       
        euler.x = -Input.GetAxis("Mouse Y") + euler.x;
        euler.y = Input.GetAxis("Mouse X") + euler.y;

        //ship.transform.eulerAngles = transform.TransformVector(euler) + ship.transform.eulerAngles;

        transform.RotateAround(ship.transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.up, Input.GetAxis("Mouse X") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.forward, -Input.GetAxis("Horizontal") * Time.deltaTime * 2);

       

    }
		
	void FixedUpdate()
    {
        if (slowing)
        {
            //Vector3 lVel = transform.InverseTransformDirection(ship.velocity);
            //ship.velocity = transform.TransformDirection(new Vector3(lVel.x * 0.95f, lVel.y * 0.95f, lVel.z));

            ship.velocity = ship.velocity - transform.TransformDirection(boostLock) * acceleration * Time.deltaTime;



        }
    }




}
