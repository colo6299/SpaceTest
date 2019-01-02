using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidFly : MonoBehaviour {

    public Rigidbody ship;
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float skidRatio = 0.8f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //simple mouse ship angle w/ rotate
        AngleShip();

    }

    void FixedUpdate()
    {
        FlightSkid();
    }


    void FlightSkid()
    {

        //clamp velocity
        ship.velocity = Vector3.ClampMagnitude(ship.velocity, maxSpeed);

        if (Input.GetAxis("Vertical") > 0)
        {

            //do the skid thing
            Vector3 calcVel = ship.velocity;
            float speed = calcVel.magnitude;
            calcVel = Vector3.Slerp(calcVel.normalized, ship.transform.forward, skidRatio * Time.deltaTime);
            calcVel *= speed;
            ship.velocity = calcVel;         

            //do the thrust thing
            ship.velocity += ship.transform.forward * Time.deltaTime * acceleration;


        }
    }


    void AngleShip()
    {
        Vector3 euler = Vector3.zero;

        euler.x = -Input.GetAxis("Mouse Y") + euler.x;
        euler.y = Input.GetAxis("Mouse X") + euler.y;

        transform.RotateAround(ship.transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.up, Input.GetAxis("Mouse X") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.forward, -Input.GetAxis("Horizontal") * Time.deltaTime * 3);
    }


}
