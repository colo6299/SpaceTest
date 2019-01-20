using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : WeaponBase {
    
    /// <summary>
    /// Damage reduction per meter
    /// </summary>
    public float DamageFalloff;

    /// <summary>
    /// Consumption per second
    /// </summary>
    public float PowerConsumption;

    /// <summary>
    /// How many time a second damage is delt
    /// </summary>
    public int TickRate;

    private float TickRateInSeconds;

    private float timeSinceLastShot;

    private Dictionary<EntityInfo, WeaponInfo> TargetDamageLogs = new Dictionary<EntityInfo, WeaponInfo>();
    private DamageTypes[] types;

    private GameObject[] Beams;
    private float range = 10000;
    private bool WasShootingLastFrame;

    void Start()
    {
        Entity = GetComponentInParent<EntityInfo>();
        Coordinator = GetComponentInParent<FireCoordinator>();

        Beams = new GameObject[Barrels.Length];
        types = Stats.Sort();
        TickRateInSeconds = 1 / (float)TickRate;
    }

    // Update is called once per frame
    void Update ()
    {
        timeSinceLastShot += Time.deltaTime;

        if (HasShootRequest() && ConsumePower())
        {
            // Display lance
            if (!WasShootingLastFrame)
            {
                CreateBeams();
                WasShootingLastFrame = true;
            }

            for (int i = 0; i < Barrels.Length; i++)
            {
                Transform barrel = Barrels[i];

                Beams[i].transform.position = barrel.position;
                Beams[i].transform.rotation = barrel.rotation;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, range, LayerMask.GetMask("Enemy") | LayerMask.GetMask("Environment")))
                {
                    EntityInfo target = hit.transform.GetComponent<EntityInfo>();

                    if (target != null)
                    {
                        if (!TargetDamageLogs.ContainsKey(target))
                        {
                            TargetDamageLogs.Add(target, new WeaponInfo() { CritChance = Stats.CritChance, CritDamage = Stats.CritDamage });
                        }

                        WeaponInfo info = TargetDamageLogs[target];

                        // convert damage from damage per second to damage per frame
                        // add it to the pending damage stack
                        foreach (DamageTypes value in types)
                        {
                            info.AddDamage(value, Stats.Damage(value) * Time.deltaTime);
                        }
                    }
                    else
                    {
                        SetBeamScale(hit.distance);
                    }
                }
                else if (Beams[0].transform.localScale.z != range)
                {
                    SetBeamScale(range);
                }
            }
        }
        else if (WasShootingLastFrame)
        {
            RemoveBeams();
            WasShootingLastFrame = false;
        }

        if (timeSinceLastShot >= TickRateInSeconds)
        {
            foreach (KeyValuePair<EntityInfo, WeaponInfo> data in TargetDamageLogs)
            {
                data.Key.TakeDamage(data.Value);
            }

            TargetDamageLogs.Clear();
            timeSinceLastShot -= TickRateInSeconds;
        }
	}


    private void CreateBeams()
    {
        for (int i = 0; i < Beams.Length; i++)
        {
            Beams[i] = Instantiate(Projectile, Barrels[i].position, Barrels[i].rotation);
            Beams[i].transform.localScale = new Vector3(1, 1, range);
        }
    }

    private void SetBeamScale(float newRange)
    {
        for (int i = 0; i < Beams.Length; i++)
        {
            Beams[i].transform.localScale = new Vector3(1, 1, newRange);
        }
    }

    private void RemoveBeams()
    {
        for (int i = 0; i < Beams.Length; i++)
        {
            Destroy(Beams[i]);
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
