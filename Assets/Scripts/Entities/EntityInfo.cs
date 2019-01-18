﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    private static System.Random rng = new System.Random((int)(DateTime.Now.Ticks / 5));

    public float Health = 2000;
    public float Energy = 2000;

    public GameObject dmgPrefab = null;

    public virtual void TakeDamage(WeaponInfo info)
    {
        double rng = EntityInfo.rng.NextDouble();

        DamageReport report = new DamageReport();
        report.Crit = rng <= info.CritChance;
        report.SetIncoming(info);

        Health -= report.TotalIncoming();
        if (Health <= 0)
        {
            Destroy(gameObject);

            if (dmgPrefab != null)
            {
                Destroy(Instantiate(dmgPrefab, transform.position, transform.rotation, null), 5f);
            }
        }
    }







}
