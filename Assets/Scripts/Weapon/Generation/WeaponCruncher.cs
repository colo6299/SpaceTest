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
        damage = power + 1/2 * rollArray[1];
        stat3 = 2 * rollArray[2] + rollArray[3] / 5;
    }



}
