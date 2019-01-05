using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    private static System.Random rng = new System.Random((int)(DateTime.Now.Ticks / 5));

    public float Health = 2000;
    Dictionary<ResistanceTypes, ResistanceInfo> Armors = new Dictionary<ResistanceTypes, ResistanceInfo>()
    {
        { ResistanceTypes.Plate, new ResistanceInfo { Armor = 10, CritChance = 0.003f, CritResistance = 1 } },
        { ResistanceTypes.Thermal, new ResistanceInfo { Armor = 10, CritChance = 0.003f, CritResistance = 1 } },
        { ResistanceTypes.Antimater, new ResistanceInfo { Armor = 10, CritChance = 0.03f, CritResistance = 1 } },
    };
    public GameObject dmgPrefab;

    

    public void TakeDamage(WeaponInfo info)
    {
        bool crit = false;
        double value = rng.NextDouble();
        if (value <= info.CritChance)
        {
            crit = true;
            info.Damage = info.Damage + (info.CritDamageMultiplier * info.Damage);
        }

        if (Armors.ContainsKey(info.DamageType))
        {
            DamageReductionReport report = Armors[info.DamageType].ReduceDamage(info.Damage);
            Debug.Log("Attack Crit: " + crit + " Value: " + value.ToString("n4") + " Damage: " + info.Damage + " Defense Crit: " + report.Crit + " Reduction: " + report.Reduction + " Final: " + report.Remaining);
            info.Damage = report.Remaining;
        }

        Health -= info.Damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
            Destroy(Instantiate(dmgPrefab, transform.position, transform.rotation, null), 5f);
        }
    }







}
