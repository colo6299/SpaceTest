using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float speed = 0.25f;
    float mouseSpeed = 1f;
    float zRotSpeed = 2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float x = Input.GetAxis("Mouse X") * mouseSpeed;
        float y = Input.GetAxis("Mouse Y") * mouseSpeed;
        transform.localRotation *= Quaternion.Euler(-y, x, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.localRotation *= Quaternion.Euler(0, 0, zRotSpeed);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.localRotation *= Quaternion.Euler(0, 0, -zRotSpeed);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward * speed * Input.GetAxis("Vertical"));
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal"));
        }


        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * 3 / 4);
        }

        if (Input.GetKey(KeyCode.C))
        {
            transform.Translate(Vector3.down * speed * 3 / 4);
        }
    }
}
