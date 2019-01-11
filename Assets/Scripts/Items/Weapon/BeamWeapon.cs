using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon: WeaponBase {
    
    /// <summary>
    /// The beam range
    /// </summary>
    public float Range = 20;

    /// <summary>
    /// Consumption per second
    /// </summary>
    public float PowerConsumption = 100;

    private bool WasShootingLastFrame;

    void Start()
    {
        Entity = GetComponentInParent<EntityInfo>();
        Coordinator = GetComponentInParent<FireCoordinator>();
    }

    // Update is called once per frame
    void Update () {

        if (HasShootRequest() && ConsumePower())
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

        if (Entity.Energy >= powerDraw)
        {
            Entity.Energy -= powerDraw;
            return true;
        }

        return false;
    }
}
