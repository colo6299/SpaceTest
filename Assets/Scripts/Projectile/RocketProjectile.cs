using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : ProjectileBase {


    public GameObject blast;

    void Update()
    {
        float distance = Speed * Time.deltaTime;
        currentTrajectory += distance;
        transform.position += ((transform.forward * distance) + inharetedVelocity * Time.deltaTime);

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 5, transform.forward, out hit, distance))
        {
            Entity ship = hit.transform.GetComponent<Entity>();

            if (ship != null)
            {
                ship.TakeDamage(Parent.Stats);
            }

            Destroy(gameObject);
        }
        else if (currentTrajectory >= MaxTrajectory)
        {
            Destroy(gameObject);
        }
    }
}
