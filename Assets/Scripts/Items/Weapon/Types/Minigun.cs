using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : ProjectileWeapon
{
    public override void RollStats(RollInfo info)
    {
        int AmmoMin = 25;
        int AmmoMax = 125;

        float ReloadMin = 1;
        float ReloadMax = 5;

        float FireRateMin = 600;
        float FireRateMax = 1000;

        info.name = info.rarity + " Minigun";
        Rarity = info.rarity;
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
