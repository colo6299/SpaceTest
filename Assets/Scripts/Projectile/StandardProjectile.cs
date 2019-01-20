﻿using UnityEngine;

public class StandardProjectile : ProjectileBase
{
    void Update()
    {
        float distance = Speed * Time.deltaTime;
        currentTrajectory += distance;
        transform.position += ((transform.forward * distance) + inharetedVelocity * Time.deltaTime);

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 5, transform.forward, out hit, distance))
        {
            EntityInfo ship = hit.transform.GetComponent<EntityInfo>();

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
