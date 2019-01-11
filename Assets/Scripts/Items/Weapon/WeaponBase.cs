using System.Text;
using UnityEngine;

public abstract class WeaponBase : ItemBase, IItem
{
    public enum FiringSystem { Sequenced, Simultaneous }

    /// <summary>
    /// How projectiles are spawned between weapon barrels
    /// </summary>
    public FiringSystem System = FiringSystem.Sequenced;

    /// <summary>
    /// Identifies this weapons slot compatability: Primary, Secondary
    /// </summary>
    public SlotType Type = SlotType.Primary;

    /// <summary>
    /// Weapon damage stats
    /// </summary>
    public WeaponInfo Stats = new WeaponInfo
    {
        DamageType = ResistanceTypes.Plate,
        Damage = 50,
        CritChance = 0.03f,
        CritDamageMultiplier = 1f
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

    public SlotType Slot()
    {
        return Type;
    }

    public virtual void RollStats(RollInfo info)
    {
    }
}
