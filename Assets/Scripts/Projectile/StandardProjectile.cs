using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour {

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

            if (hit.transform.gameObject.GetComponent<DestroyableCoordinator>() != null)
            {
                DestroyableCoordinator ship = hit.transform.GetComponent<DestroyableCoordinator>();
                ship.TakeDamage(Damage);              
            }

            Destroy(gameObject);


        }
        else if (CurrentTrajectory >= MaxTrajectory)
        {
            Destroy(gameObject);
        }
	}
}
