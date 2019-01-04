using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidFly : MonoBehaviour {

    public Rigidbody ship;
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float skidRatio = 0.8f;
    public EntityInfo info;

    private float xavg;
    private float yavg;
    private float zavg;


    void Start()
    {

        info = GetComponent<EntityInfo>();

        UpdateInfo();

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

    public void UpdateInfo()
    {
        acceleration = info.Acceleration;
        maxSpeed = info.MaxSpeed;
        skidRatio = info.SkidRatio;
    }

    void FlightSkid()
    {

        //clamp velocity
        ship.velocity = Vector3.ClampMagnitude(ship.velocity, maxSpeed);

        if (!Input.GetKey(KeyCode.LeftShift))
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

        else
        {
           
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

        ship.angularVelocity = ship.transform.TransformDirection(rotVec) * Time.deltaTime * 10 + ship.angularVelocity;

        ship.maxAngularVelocity = 5f;


        //transform.RotateAround(ship.transform.right, rotx  * Time.deltaTime * 2);
        //transform.RotateAround(ship.transform.up, roty  * Time.deltaTime * 2);
        //transform.RotateAround(ship.transform.forward, rotz  * Time.deltaTime * 3.5f);
    }


}
