using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public enum FiringSystem { Sequenced, Simultaneous }
    public enum WeaponType { Primary, Secondary }

    /// <summary>
    /// How projectiles are spawned between weapon barrels
    /// </summary>
    public FiringSystem System = FiringSystem.Sequenced;

    /// <summary>
    /// What weapon slot does this fit in. (Will be moved somewhere else)
    /// </summary>
    public WeaponType Type = WeaponType.Primary;

    /// <summary>
    /// Shots per minute
    /// 0 or less means shoot every update
    /// </summary>
    public float RateOfFire = 400;

    /// <summary>
    /// Number of shots before reload
    /// 0 or less means no reload
    /// </summary>
    public int Ammunition = 0;
    
    /// <summary>
    /// Reload Time in seconds
    /// 0 or less means no reload
    /// </summary>
    public float ReloadTime = 0;

    public GameObject Projectile;
    public Transform[] Barrels;

    protected int currentBarrelIndex = 0;
    protected double timeTillNextShot = 1;
    protected int currentAmmunition;
    protected float currentReloadTime = 0;

    private FireCoordinator coordinator = null;

    public bool IsReadyToFire()
    {
        return timeTillNextShot >= 1 && !IsReloading();
    }

    public bool IsReloading()
    {
        return currentReloadTime > 0;
    }

    public bool ShootEveryFrame()
    {
        return RateOfFire <= 0;
    }

    private void Start()
    {
        currentAmmunition = Ammunition;
        coordinator = gameObject.GetComponent<FireCoordinator>();

        if (coordinator == null)
        {
            Debug.Log("FireCoordinator was not found on: " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if primary and is not fireing or secondary and is not firing, do nothing
        if (coordinator != null &&
            ((Type == WeaponType.Primary && !coordinator.IsPrimaryFiring) ||
            (Type == WeaponType.Secondary && !coordinator.IsSecondaryFiring))) return;

        if (IsReloading())
        {
            currentReloadTime -= Time.deltaTime;
            currentAmmunition = Ammunition;
        }

        if (ShootEveryFrame())
        {
            timeTillNextShot = 1;
        }
        else
        {
            if (IsReloading() && timeTillNextShot > 1)
            {
                timeTillNextShot = 1;
            }
            else
            {
                timeTillNextShot += Time.deltaTime * RateOfFire / 60;
            }
        }

        // If the rate of fire is high enough, more than one bullet may need to be spawned per frame
        while (IsReadyToFire())
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

    private void FireNextInSequence()
    {
        // ensures the game does not crash if array is not initialized
        if (Barrels != null && Barrels.Length > 0)
        {
            Instantiate(Projectile, Barrels[currentBarrelIndex].position, Barrels[currentBarrelIndex].rotation);

            currentBarrelIndex++;
            if (currentBarrelIndex == Barrels.Length)
            {
                currentBarrelIndex = 0;
            }

            ConsumeAmmo();
            ReduceTimeTillNextShot();
        }
    }

    private void FireSimultaneous()
    {
        foreach (Transform t in Barrels)
        {
            Instantiate(Projectile, t.position, t.rotation);
            ConsumeAmmo();
        }

        ReduceTimeTillNextShot();
    }

    /// <summary>
    /// Reduces ammo and starts a reload if ammo  reaches 0
    /// </summary>
    private void ConsumeAmmo(int count = 1)
    {
        currentAmmunition -= count;

        if (currentAmmunition <= 0)
        {
            currentReloadTime = ReloadTime;
        } 
    }

    private void ReduceTimeTillNextShot()
    {
        timeTillNextShot -= 1;
    }
}
