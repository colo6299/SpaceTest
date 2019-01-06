using System.Text;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public enum FiringSystem { Sequenced, Simultaneous }
    public enum WeaponType { Primary, Secondary }

    /// <summary>
    /// The weapon's power level
    /// I wouldn't recommend setting this below zero
    /// </summary>
    public int Power = 0;

    /// <summary>
    /// The weapon's per bullet damage
    /// I wouldn't recommend setting this below zero
    /// </summary>
    public float Damage = 0;

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

    public WeaponInfo Stats = new WeaponInfo
    {
        DamageType = ResistanceTypes.Plate,
        Damage = 50,
        CritChance = 0.03f,
        CritDamageMultiplier = 1f
    };

    /// <summary>
    /// How projectiles are spawned between weapon barrels
    /// </summary>
    public FiringSystem System = FiringSystem.Sequenced;

    /// <summary>
    /// What weapon slot does this fit in. (Will be moved somewhere else)
    /// </summary>
    public WeaponType Type = WeaponType.Primary;

    public GameObject Projectile;
    public Transform[] Barrels;

    protected int currentBarrelIndex = 0;
    protected double timeTillNextShot = 1;
    protected int currentAmmunition;
    protected float currentReloadTime = 0;
    protected float idleReloadTime = 0;

    private FireCoordinator coordinator = null;

    public RollInfo rollInfo;
    private int rollIterator;
    private float[] rollArray;

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

    private bool CanFire()
    {
        return coordinator != null &&
            ((Type == WeaponType.Primary && coordinator.IsPrimaryFiring) ||
            (Type == WeaponType.Secondary && coordinator.IsSecondaryFiring));
    }

    private void Start()
    {
        currentAmmunition = Ammunition;
        if (transform.parent.parent.parent.GetComponent<FireCoordinator>() != null)
        {
            coordinator = transform.parent.parent.parent.GetComponent<FireCoordinator>();
        }
        else
        {
            coordinator = gameObject.GetComponent<FireCoordinator>();
        }

        if (coordinator == null)
        {
            Debug.Log("FireCoordinator was not found on: " + gameObject.name);
        }
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

        if (ShootEveryFrame())
        {
            timeTillNextShot = 1;
        }
        else
        {
            if (timeTillNextShot > 1 && (IsReloading() || !CanFire()))
            {
                timeTillNextShot = 1;
            }
            else
            {
                timeTillNextShot += Time.deltaTime * RateOfFire / 60;
            }
        }

        // If the rate of fire is high enough, more than one bullet may need to be spawned per frame
        while (CanFire() && IsReadyToFire())
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

        if (CanFire())
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
            ReduceTimeTillNextShot();
        }
    }

    private void FireSimultaneous()
    {
        foreach (Transform t in Barrels)
        {
            FireProjectile(t);
            ConsumeAmmo();
        }

        ReduceTimeTillNextShot();
    }

    public void FireProjectile(Transform tran)
    {
        ProjectileBase projectile = Instantiate(Projectile, tran.position, tran.rotation).GetComponent<ProjectileBase>();
        projectile.Parent = gameObject.GetComponentInParent<Rigidbody>();
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

    private void ReduceTimeTillNextShot()
    {
        timeTillNextShot -= 1;
    }

    protected float Roll()
    {
        rollIterator++;
        return rollArray[rollIterator];
    }

    protected void StartRolling(RollInfo _Info)
    {
        rollInfo = _Info;
        rollArray = _Info.rollArray;
        rollIterator = -1;
    }

}
