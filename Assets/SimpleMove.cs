using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

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

    void Update()
    {

        //I know it's still messy no h8 just testing again

        slowing = !Input.GetKey(KeyCode.LeftShift);

        ship.velocity += transform.TransformDirection(new Vector3(0, 0, acceleration * Time.deltaTime * Input.GetAxis("Vertical")));

        Vector3 euler = Vector3.zero;

        euler.x = -Input.GetAxis("Mouse Y") + euler.x;
        euler.y = Input.GetAxis("Mouse X") + euler.y;

        //ship.transform.eulerAngles = transform.TransformVector(euler) + ship.transform.eulerAngles;

        transform.RotateAround(ship.transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.up, Input.GetAxis("Mouse X") * Time.deltaTime * 2);
        transform.RotateAround(ship.transform.forward, -Input.GetAxis("Horizontal") * Time.deltaTime * 3);


    }

    void FixedUpdate()
    {
        if (slowing)
        {
            Vector3 sVel = transform.InverseTransformDirection(ship.velocity);
            sVel.x *= 0.95f;
            sVel.y *= 0.95f;
            ship.velocity = transform.TransformDirection(sVel);
        }
    }
}
