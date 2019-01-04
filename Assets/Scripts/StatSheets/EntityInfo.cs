using Assets.Scripts.StatSheets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    private static System.Random rng = new System.Random((int)(DateTime.Now.Ticks / 5));

    public float Health = 2000;
    Dictionary<DamageAndResistances, ResistanceInfo> Armors = new Dictionary<DamageAndResistances, ResistanceInfo>()
    {
        { DamageAndResistances.Plate, new ResistanceInfo { Armor = 10, CritChance = 0.003f, CritResistance = 1 } },
        { DamageAndResistances.Thermal, new ResistanceInfo { Armor = 10, CritChance = 0.003f, CritResistance = 1 } },
        { DamageAndResistances.Antimater, new ResistanceInfo { Armor = 10, CritChance = 0.03f, CritResistance = 1 } },
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

        if (Armors.ContainsKey(info.Type))
        {
            DamageReductionReport report = Armors[info.Type].ReduceDamage(info.Damage);
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
