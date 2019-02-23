using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{

    /// <summary>
    /// Shots per minute
    /// </summary>
    public float RateOfFire = 400;

    /// <summary>
    /// Number of shots before reload
    /// 0 or less means no reload
    /// </summary>
    public int Ammunition = 100;

    /// <summary>
    /// Reload Time in seconds
    /// </summary>
    public float ReloadTime = 1;

    /// <summary>
    /// Shot deviation
    /// </summary>
    public float Accuracy = 1f;

    protected int currentBarrelIndex = 0;
    protected double timeTillNextShot = 1;
    protected int currentAmmunition;
    protected float currentReloadTime = 0;
    protected float idleReloadTime = 0;

    public bool IsReadyToFire()
    {
        return timeTillNextShot >= 1 && !IsReloading();
    }

    public bool IsReloading()
    {
        return currentReloadTime > 0;
    }

    public override float DPS()
    {

        float shotsPerSecond = RateOfFire / 60;

        if (Ammunition == 0 || ReloadTime == 0)
        {
            return Stats.TotalDamage() * shotsPerSecond;
        }

        float shotInterval = 1 / shotsPerSecond;

        float burstDuration = shotInterval * Ammunition;

        float totalTime = ReloadTime + burstDuration;

        return Stats.TotalDamage() * Ammunition / totalTime;
    }

    void Start()
    {
        Entity = GetComponentInParent<Entity>();
        Coordinator = GetComponentInParent<FireCoordinator>();
        currentAmmunition = Ammunition;
    }

    // Update is called once per frame
    void Update()
    {
        ReloadUpdate();

        // charge for next sho
        if (timeTillNextShot > 1 && (IsReloading() || !HasShootRequest()))
        {
            timeTillNextShot = 1;
        }
        else
        {
            timeTillNextShot += Time.deltaTime * (RateOfFire / 60) / (System == FiringSystem.Simultaneous ? Barrels.Length : 1);
        }

        // If the rate of fire is high enough, more than one bullet may need to be spawned per frame
        while (HasShootRequest() && IsReadyToFire())
        {
            switch (System)
            {
                case FiringSystem.Sequenced:
                    FireNextInSequence();
                    break;
                case FiringSystem.Simultaneous:
                    FireSimultaneous();
                    break;
            }
        }

    }

    public void ReloadUpdate()
    {
        if (IsReloading())
        {
            currentReloadTime -= Time.deltaTime;
        }

        if (!IsReloading() && currentAmmunition <= 0)
        {
            currentAmmunition = Ammunition;
        }

        if (HasShootRequest())
        {
            idleReloadTime = 0;
        }
        else
        {
            idleReloadTime += Time.deltaTime;

            if (idleReloadTime >= ReloadTime)
            {
                currentAmmunition = Ammunition;
                idleReloadTime = 0;
            }
        }
    }

    private void FireNextInSequence()
    {
        // ensures the game does not crash if array is not initialized
        if (Barrels != null && Barrels.Length > 0)
        {
            FireProjectile(Barrels[currentBarrelIndex]);

            currentBarrelIndex++;
            if (currentBarrelIndex == Barrels.Length)
            {
                currentBarrelIndex = 0;
            }

            ConsumeAmmo();
            timeTillNextShot -= 1;
        }
    }

    private void FireSimultaneous()
    {
        foreach (Transform t in Barrels)
        {
            FireProjectile(t);
            ConsumeAmmo();
        }

        timeTillNextShot -= 1;
    }

    public void FireProjectile(Transform tran)
    {
        ProjectileBase projectile = Instantiate(Projectile, tran.position, tran.rotation).GetComponent<ProjectileBase>();
        projectile.transform.forward = Deviate(projectile.transform.forward, Accuracy);
        projectile.Parent = gameObject.GetComponent<WeaponBase>();
    }

    /// <summary>
    /// Reduces ammo count
    /// </summary>
    private void ConsumeAmmo(int count = 1)
    {
        currentAmmunition -= count;

        if (currentAmmunition <= 0)
        {
            currentReloadTime = ReloadTime;
        }
    }
}
