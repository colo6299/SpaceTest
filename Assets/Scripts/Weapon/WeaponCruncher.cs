using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCruncher : MonoBehaviour {


    public float[] rollArray;

    public float power;
    public float damage;
    public float stat3;


    void Start()
    {
        power = rollArray[0];
        damage = rollArray[0];
    }



}
