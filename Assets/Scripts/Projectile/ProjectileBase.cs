using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public WeaponBase Parent;

    public float MaxTrajectory = 1000f;
    public float Speed = 50f;

    protected Vector3 inharetedVelocity;
    protected float currentTrajectory = 0;

    private void Start()
    {
        inharetedVelocity = Parent.GetComponentInParent<Rigidbody>().velocity;
    }

    // Update is called once per frame


}
