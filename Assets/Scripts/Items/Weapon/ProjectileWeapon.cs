using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ProjectileWeapon : WeaponBase, IItem
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
    public float ReloadTime = 1000;

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

    void Start()
    {
        Entity = GetComponentInParent<EntityInfo>();
        Coordinator = GetComponentInParent<FireCoordinator>();
        currentAmmunition = Ammunition;

    }

    void OnGUI()
    {
        if (Application.isEditor)  // or check the app debug flag
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Type: " + Type.ToString());
            sb.AppendLine("System: " + System.ToString());
            sb.AppendLine("Projectile: " + Projectile.GetType().Name);
            sb.AppendLine(string.Format("Shot: {0}/{1}", currentAmmunition, Ammunition));
            sb.AppendLine(string.Format("Reload: {0}/{1}", currentReloadTime.ToString("n2"), ReloadTime.ToString("n2")));
            sb.AppendLine(string.Format("Idle Reload: {0}/{1}", idleReloadTime.ToString("n2"), ReloadTime.ToString("n2")));
            GUI.Label(new Rect(10, 0, 500, 500), sb.ToString());
        }
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
            timeTillNextShot += Time.deltaTime * RateOfFire / 60;
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
