using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {


    public GameObject player;

    private ShipInfo info;
    private float speed;

    private string navState = "Standby";
    private float distance;
    private Rigidbody prb;
    private Rigidbody rb;
    private float minStrafe = 100f;
    private float minStart = 400f;
    private float maxStart = 800f;

    private bool strafing = false;
    private bool stalking = false;
    private bool slewing = false;
    private bool retreating = false;

    private Vector3 playerPrediction;
    private Vector3 targetPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prb = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        info = GetComponent<ShipInfo>();
        UpdateStats();
    }


    void Update()
    {
        targetPoint = playerPrediction;

        NavChoice();

        if (slewing) { MoveSlew(); }
        else { MoveStandard(); }

    }


    public void UpdateStats()
    {
        speed = info.MaxSpeed;
    }


    void NavChoice()
    {
        distance = (player.transform.position - transform.position).magnitude;
        playerPrediction = player.transform.position + (prb.velocity - rb.velocity);

        if (distance < minStrafe)
        {
            strafing = false;
        }

        if (distance > minStart)
        {
            strafing = true;
        }

        if (strafing)
        {
            Strafe();
        }

        else if (distance < maxStart)
        {
            Retreat();
        }

        else
        {
            Stalk();
        }
    }


    void FireControl()
    {
        if (strafing)
        {
            Fire();
        }
    }


    void Fire()
    {

    }


    void MoveStandard()
    {
        rb.velocity = transform.forward * speed;

        Vector3 rotVec = transform.InverseTransformPoint(targetPoint);
        rotVec = new Vector3(-rotVec.y, rotVec.x, 0);

        Debug.Log(targetPoint);

        rb.angularVelocity = transform.TransformDirection(rotVec.normalized);

    }


    void MoveSlew()
    {

    }


    void Strafe()
    {

    }


    void Stalk()
    {

    }


    void Retreat()
    {
         
    }



}
