using System;
using UnityEngine;

public class EntityInfo : MonoBehaviour
{

    public static event Action<EntityInfo, DamageReport> DamageReport;

    private static System.Random rng = new System.Random((int)(DateTime.Now.Ticks / 5));

    public float MaxHealth = 2000;
    public float Health = 2000;

    public float MaxEnergy = 2000;
    public float Energy = 2000;
    public float EnergyRegen = 25;
    public float EnergyCooldownTime = 2;

    private bool EnergyOnCooldown = false;
    private float currentEnergyTime = 0;

    public GameObject DamagePrefab;
    public EntityStatusBar StatusBar;
    public GameObject Loot;

    public virtual void Update()
    {
        if (EnergyOnCooldown)
        {
            currentEnergyTime += Time.deltaTime;

            if (currentEnergyTime >= EnergyCooldownTime)
            {
                currentEnergyTime = 0;
                EnergyOnCooldown = false;
            }
        }
        else
        {
            // recover
            if (Energy < MaxEnergy)
            {
                Energy += EnergyRegen * Time.deltaTime;

                if (Energy > MaxEnergy)
                {
                    Energy = MaxEnergy;
                }
            }
        }

        if (StatusBar != null)
            StatusBar.UpdateBars(this);
    }

    public virtual void TakeDamage(WeaponInfo info)
    {
        double rng = EntityInfo.rng.NextDouble();

        DamageReport report = new DamageReport();
        report.Crit = rng <= info.CritChance;
        report.SetIncoming(info);

        Health -= report.TotalIncoming();

        // report damage taken
        if (DamageReport != null)
        {
            DamageReport.Invoke(this, report);
        }

        if (Health <= 0)
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

    public virtual bool ConsumeEnergy(float amount)
    {
        if (amount <= Energy)
        {
            Energy -= amount;
            currentEnergyTime = 0;
            EnergyOnCooldown = true;
            return true;
        }

        return false;
    }
}
