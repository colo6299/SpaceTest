﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
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
    private float dodgeScalar = 10f;
    private float dodgeTime = 0f;
    private float dodgeDelay = 3f; //Time after dodging before tracking player

    private bool strafing = false;
    private bool stalking = false;
    private bool slewing = false;
    private bool retreating = false;
    private bool dodging = false;

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
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, 100f, 1 << 10);

        if (Time.time - dodgeTime > dodgeDelay)
        {
            targetPoint = playerPrediction;
        }
        
        if (hit.distance < 40f)
        {
            dodgeTime = Time.time;
            Vector3 normal = transform.InverseTransformPoint(hit.normal);
            normal = normal.normalized * dodgeScalar;
            normal.z = hit.distance;
            normal = transform.TransformPoint(normal);
            targetPoint = normal;
        }

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
