using System.Text;
using UnityEngine;

public abstract class WeaponBase : Item
{
    public enum FiringSystem { Sequenced, Simultaneous }

    public const float MaxDeviation = 0.01f;

    /// <summary>
    /// How projectiles are spawned between weapon barrels
    /// </summary>
    public FiringSystem System = FiringSystem.Sequenced;

    /// <summary>
    /// Weapon damage stats
    /// </summary>
    public WeaponInfo Stats = new WeaponInfo
    {
        CritChance = 0.03f,
        CritDamage = 1f
    };

    /// <summary>
    /// projectile spawn locations
    /// </summary>
    public Transform[] Barrels;

    /// <summary>
    /// The prefab object that is spawned when the weapon fires
    /// </summary>
    public GameObject Projectile;

    /// <summary>
    /// The stat details for this ship/entity
    /// </summary>
    protected EntityInfo Entity;
    protected FireCoordinator Coordinator;

    // Use this for initialization
    void Start()
    {
        Entity = GetComponentInParent<EntityInfo>();
        Coordinator = GetComponentInParent<FireCoordinator>();

        Stats.SetDamage(DamageTypes.Standard, 50);
    }

    /// <summary>
    /// Returns true if the user is requesting to firing this weapon
    /// </summary>
    /// <returns></returns>
    public bool HasShootRequest()
    {
        return Coordinator != null &&
            ((Type == SlotType.Primary && Coordinator.IsPrimaryFiring) ||
            (Type == SlotType.Secondary && Coordinator.IsSecondaryFiring));
    }

    public virtual float DPS()
    {
        return 0;
    }

    public static Vector3 Deviate(Vector3 direction, float accuracy)
    {

        float deviation = MaxDeviation - (accuracy * MaxDeviation);

        float x = Random.Range(-deviation, deviation);
        float y = Random.Range(-deviation, deviation);
        float z = Random.Range(-deviation, deviation);

        return new Vector3(direction.x + x, direction.y + y, direction.z + z);

    }
}
