using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour {

    public float acceleration = 10;

    private bool slowing = false;
    private bool boosting = false;

    private Vector3 boostLock;

    public Rigidbody ship;
    public Transform engine;
    public Transform pointer;

    public GameObject blueFlame;
    public GameObject orangeFlame;

    private Vector3 invDir;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Update ()
    {

        //I know it's messy no h8 just testing its feel
        blueFlame.SetActive(Input.GetKey(KeyCode.Space));
        //orangeFlame.SetActive(Input.GetKey(KeyCode.LeftShift));

        ship.velocity += transform.TransformDirection(new Vector3(0, 0, acceleration * Time.deltaTime * Input.GetAxis("Vertical")));

        invDir = transform.InverseTransformDirection(ship.velocity);
        invDir.z = 0;
        invDir = invDir.normalized;

        if (!slowing & Input.GetKey(KeyCode.LeftShift))
        {
            boostLock = invDir;
        }
        slowing = Input.GetKey(KeyCode.LeftShift);
        boosting = Input.GetKey(KeyCode.Space);

        if (!Input.GetKey(KeyCode.Space))
        {
            //slowing = Input.GetKey(KeyCode.LeftShift);
            orangeFlame.SetActive(Input.GetKey(KeyCode.LeftShift));
        }
        else
        {
            blueFlame.SetActive(true);
            orangeFlame.SetActive(false);
        }


        Vector3 euler = Vector3.zero;
       
        euler.x = -Input.GetAxis("Mouse Y") + euler.x;
        euler.y = Input.GetAxis("Mouse X") + euler.y;

        //ship.transform.eulerAngles = transform.TransformVector(euler) + ship.transform.eulerAngles;

        transform.RotateAround(ship.transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.up, Input.GetAxis("Mouse X") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.forward, -Input.GetAxis("Horizontal") * Time.deltaTime * 3);

        Debug.Log(boosting);
        if (!slowing)
        {
            engine.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(invDir.y, invDir.x) * 180 / Mathf.PI);
        }
        pointer.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(invDir.y, invDir.x) * 180 / Mathf.PI);


    }
		
	void FixedUpdate()
    {
        if (slowing & !boosting)
        {
            //Vector3 lVel = transform.InverseTransformDirection(ship.velocity);
            //ship.velocity = transform.TransformDirection(new Vector3(lVel.x * 0.95f, lVel.y * 0.95f, lVel.z));

            ship.velocity = ship.velocity - transform.TransformDirection(boostLock) * acceleration * Time.deltaTime;
        }
        if (boosting)
        {
            if (slowing)
            {
                ship.velocity = ship.velocity + transform.TransformDirection(boostLock) * acceleration * Time.deltaTime / 2;
            }
            else
            {
                ship.velocity = ship.velocity + transform.TransformDirection(invDir) * acceleration * Time.deltaTime / 2;
            }
            
        }
    }




}
