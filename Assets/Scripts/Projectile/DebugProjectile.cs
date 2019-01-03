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
        float distance = Speed * Time.deltaTime;
        CurrentTrajectory += distance;
        transform.position += transform.forward * distance;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            BasicShipInfo ship = hit.transform.GetComponent<BasicShipInfo>();

            ship.DoDamage(Damage);
            Destroy(gameObject);
        }
        else if (CurrentTrajectory >= MaxTrajectory)
        {
            Destroy(gameObject);
        }
	}
}
