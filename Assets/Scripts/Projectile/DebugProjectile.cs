using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugProjectile : MonoBehaviour {

    public float MaxTrajectory = 1000f;
    public float Damage = 10f;
    public float Speed = 50f;

    private float CurrentTrajectory = 0;

    // Update is called once per frame
    void Update ()
    {
        CurrentTrajectory += Speed * Time.deltaTime;
        transform.position += transform.forward * Speed * Time.deltaTime;

        if (CurrentTrajectory >= MaxTrajectory)
        {
            Destroy(gameObject);
        }
	}
}
