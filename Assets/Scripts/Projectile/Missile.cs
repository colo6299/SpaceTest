using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase {

    public GameObject target = null;

    public float MaxTurnAngle = 180f;

    public void Update () {

        if (target == null)
        {
            target = GetTarget(); // add a cooldown to this so that it is not updating every time when no enemies are present
        }


        if (target != null)
        {
            Vector3 heading = target.transform.position - transform.position;
            float angle = Vector3.Angle(heading, transform.forward);

            Quaternion targetRotation = Quaternion.LookRotation(heading);

            float turnRate = MaxTurnAngle * Time.deltaTime;

            if (angle > turnRate)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnRate / angle);
            }
            else
            {
                transform.rotation = targetRotation;
            }
        }

        float distance = Speed * Time.deltaTime;
        currentTrajectory += distance;
        transform.position += (transform.forward * distance) + inharetedVelocity * Time.deltaTime;

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

    /// <summary>
    /// finds the nearest enemy target
    /// </summary>
    /// <returns></returns>
    private GameObject GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject current = null;
        float currentDist = 0;
        foreach (GameObject enemy in enemies)
        {
            if (current == null)
            {
                current = enemy;
                currentDist = (transform.position - current.transform.position).sqrMagnitude;
            }
            else
            {
                float enemyDist = (transform.position - enemy.transform.position).sqrMagnitude;

                if (enemyDist < currentDist)
                {
                    current = enemy;
                    currentDist = (transform.position - current.transform.position).sqrMagnitude;
                }
            }
        }

        return current;
    }
}
