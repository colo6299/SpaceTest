using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : ProjectileWeapon
{
    public int AmmoMin = 10;
    public int AmmoMax = 25;

    public float ReloadMin = 2;
    public float ReloadMax = 3;

    public float FireRateMin = 100;
    public float FireRateMax = 150;

    public override void RollStats(RollInfo info)
    {
        base.RollStats(info);

        info.name = info.rarity + " Missile Launcher";
        name = info.name;

        // this has been moved to item.cs
        //Rarity = info.rarity;
        //StartRolling(info);
        //PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = (int)(AmmoMin + Mathf.Abs(Roll()) * (AmmoMax - AmmoMin));
        ReloadTime = ReloadMin + Mathf.Abs(Roll()) * (ReloadMax - ReloadMin);
        Accuracy = Mathf.Abs(Roll());

        Stats.SetDamage(DamageTypes.Kinetic, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1;
    }

}
