using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{
    public enum FiringSystem { Sequenced, Simultaneous }

    public bool IsFireing = false;

    public FiringSystem System = FiringSystem.Sequenced;
    public float RateOfFire = 400; // shots per minute

    public GameObject Projectile;
    public Transform[] Barrels;

    private int currentBarrelIndex = 0;
    private double timeTillNextShot = 1;

    public bool IsReadyToFire()
    {
        return timeTillNextShot >= 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing if you are not firing
        if (!IsFireing) return;

        // update time interval
        timeTillNextShot += Time.deltaTime * RateOfFire / 60;

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
    /// Consumes ammo
    /// </summary>
    private void ConsumeAmmo()
    {
        
    }

    private void ReduceTimeTillNextShot()
    {
        timeTillNextShot -= 1;
    }
}
