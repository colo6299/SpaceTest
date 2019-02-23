using Assets.Scripts.Entities;
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public static event Action<Entity, DamageReport> DamageReport;

    private static System.Random rng = new System.Random((int)(DateTime.Now.Ticks / 5));

    // Prefab variables
    public float HealthMax = 1000;
    public float HealthCurrent = 1000;
    public float HealthRegen = 0;
    public float HealthCooldown = 0;

    public float EnergyMax = 1000;
    public float EnergyCurrent = 1000;
    public float EnergyRegen = 50;
    public float EnergyCooldown = 3;

    public float BoostMax = 1000;
    public float BoostCurrent = 1000;
    public float BoostRegen = 50;
    public float BoostCooldown = 3;

    public GameObject DamagePrefab;
    public EntityStatusBar StatusBar;
    public GameObject Loot;

    // general variables
    public Regenerator Health;
    public Regenerator Energy;
    public Regenerator Boost;

    public virtual void Start()
    {
        Health = new Regenerator(HealthCurrent, HealthMax, HealthRegen, HealthCooldown);
        Energy = new Regenerator(EnergyCurrent, EnergyMax, EnergyRegen, EnergyCooldown);
        Boost = new Regenerator(BoostCurrent, BoostMax, BoostRegen, BoostCooldown);

        Health.OnEmpty += DestroyEntity;
    }

    public virtual void Update()
    {
        Health.Regenerate();
        Energy.Regenerate();
        Boost.Regenerate();

        if (StatusBar != null)
            StatusBar.UpdateBars(this);
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void TakeDamage(WeaponInfo info)
    {
        double rng = Entity.rng.NextDouble();

        DamageReport report = new DamageReport();
        report.Crit = rng <= info.CritChance;
        report.SetIncoming(info);

        Health.Value -= report.TotalIncoming();
        Health.BeginOrResetCooldown();

        // report damage taken
        if (DamageReport != null)
        {
            DamageReport.Invoke(this, report);
        }
    }

    public virtual void DestroyEntity()
    {
        Destroy(gameObject);

        if (DamagePrefab != null)
        {
            Destroy(Instantiate(DamagePrefab, transform.position, transform.rotation, null), 5f);

            if (UnityEngine.Random.Range(0, 1) < 0.5f)
            {
                GameObject spawn = Instantiate(Loot, transform.position, transform.rotation, null);
                spawn.GetComponent<PickupItem>().roll = BaseRoller.RollItem(120);
            }
        }
    }
}
