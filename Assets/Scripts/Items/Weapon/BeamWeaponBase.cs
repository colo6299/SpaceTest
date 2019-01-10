using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeaponBase : ItemBase, IItem {
    
    /// <summary>
    /// The beam range
    /// </summary>
    public float Range = 20;

    /// <summary>
    /// Consumption per second
    /// </summary>
    public float PowerConsumption = 100;

    /// <summary>
    /// The slot this item fits into
    /// </summary>
    public SlotType Type = SlotType.Primary;

    public WeaponInfo Stats = new WeaponInfo
    {
        DamageType = ResistanceTypes.Thermal,
        Damage = 200,
        CritChance = 0.01f,
        CritDamageMultiplier = 1
    };

    public GameObject Beam;
    public Transform[] Barrels;


    private EntityInfo entity;
    private FireCoordinator coordinator;

    private bool WasShootingLastFrame;

    // Use this for initialization
    void Start () {

        entity = GetComponentInParent<EntityInfo>();
        coordinator = GetComponentInParent<FireCoordinator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (UserShootRequested() && ConsumePower())
        {
            if (!WasShootingLastFrame)
            {
                // Spawn beam lance
                // If we plan to draw it each frame then we dont need this.

                WasShootingLastFrame = true;
            }

            foreach (Transform barrel in Barrels)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
                {
                    EntityInfo ship = hit.transform.GetComponent<EntityInfo>();

                    if (ship != null)
                    {

                        ship.TakeDamage(new WeaponInfo
                        {
                            DamageType = ResistanceTypes.Thermal,
                            CritChance = Stats.CritChance,
                            CritDamageMultiplier = Stats.CritDamageMultiplier,
                            Damage = Stats.Damage * Time.deltaTime,
                        });
                    }
                }
            }
        }
        else
        {
            WasShootingLastFrame = false;
        }

	}

    private bool ConsumePower()
    {
        float powerDraw = PowerConsumption * Time.deltaTime;

        if (entity.Energy >= powerDraw)
        {
            entity.Energy -= powerDraw;
            return true;
        }

        return false;
    }

    private bool UserShootRequested()
    {
        return coordinator != null &&
            ((Type == SlotType.Primary && coordinator.IsPrimaryFiring) ||
            (Type == SlotType.Secondary && coordinator.IsSecondaryFiring));
    }

    public SlotType Slot()
    {
        return Type;
    }

    public void RollStats(RollInfo info)
    {
        StartRolling(info);
        PowerLevel = Mathf.RoundToInt(Roll());
        Range = Mathf.Abs(Roll() * PowerLevel) + 1;
        Stats.Damage = PowerLevel * (Roll() * 5 + 40);

    }
}
