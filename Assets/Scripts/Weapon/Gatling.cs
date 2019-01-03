using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MonoBehaviour
{
    public GameObject Projectile;
    public Transform Weapon;
    public float RateOfFire = 400;

    private double timeTillNextShot = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && timeTillNextShot >= 1)
        {
            Instantiate(Projectile, Weapon.position, Weapon.rotation);
            timeTillNextShot = 0;
        }

        timeTillNextShot += Time.deltaTime * RateOfFire;

    }
}
