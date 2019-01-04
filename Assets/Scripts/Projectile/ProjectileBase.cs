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

        private Vector3 inharetedVelocity;
        private float currentTrajectory = 0;

        private void Start()
        {
            inharetedVelocity = Parent.velocity;
        }

        // Update is called once per frame
        void Update()
        {
            float distance = Speed * Time.deltaTime;
            currentTrajectory += distance;
            transform.position += ((transform.forward * distance) + inharetedVelocity * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                EntityInfo ship = hit.transform.GetComponent<EntityInfo>();

                ship.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if (currentTrajectory >= MaxTrajectory)
            {
                Destroy(gameObject);
            }
        }

    }
}
