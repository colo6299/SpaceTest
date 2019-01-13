using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public WeaponBase Parent;

    public float MaxTrajectory = 1000f;
    public float Speed = 50f;

    private Vector3 inharetedVelocity;
    private float currentTrajectory = 0;

    private void Start()
    {
        inharetedVelocity = Parent.GetComponentInParent<Rigidbody>().velocity;
    }

    // Update is called once per frame
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
