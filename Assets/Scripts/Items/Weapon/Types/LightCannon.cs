using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCannon : ProjectileWeapon {

    public override void RollStats(RollInfo info)
    {
        int AmmoMin = 10;
        int AmmoMax = 25;

        float ReloadMin = 2;
        float ReloadMax = 3;

        float FireRateMin = 100;
        float FireRateMax = 150;

        info.name = info.rarity + " Light Cannon";
        StartRolling(info);
        name = info.name;
        PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = (int)(AmmoMin + Mathf.Abs(Roll()) * (AmmoMax - AmmoMin));
        ReloadTime = ReloadMin + Mathf.Abs(Roll()) * (ReloadMax - ReloadMin);

        Stats.Damage = PowerLevel + (PowerLevel / 4) * Roll(); //PowerLevel * (Roll() * 5 + 40);
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamageMultiplier = 1;
    }
}
