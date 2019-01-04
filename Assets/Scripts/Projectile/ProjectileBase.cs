using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public Rigidbody Parent;

        public float MaxTrajectory = 1000f;
        public float Damage = 10f;
        public float Speed = 50f;

        private float CurrentTrajectory = 0;

        private Vector3 inharetedVelocity;

        private void Start()
        {
            inharetedVelocity = Parent.velocity;
        }

        // Update is called once per frame
        void Update()
        {
            float distance = Speed * Time.deltaTime;
            CurrentTrajectory += distance;
            transform.position += ((transform.forward * distance) + inharetedVelocity * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                DestroyableCoordinator ship = hit.transform.GetComponent<DestroyableCoordinator>();

                ship.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if (CurrentTrajectory >= MaxTrajectory)
            {
                Destroy(gameObject);
            }
        }

    }
}
