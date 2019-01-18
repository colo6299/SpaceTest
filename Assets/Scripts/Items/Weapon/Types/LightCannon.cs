using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCannon : ProjectileWeapon
{

    public override void RollStats(RollInfo info)
    {
        int AmmoMin = 10;
        int AmmoMax = 25;

        float ReloadMin = 2;
        float ReloadMax = 3;

        float FireRateMin = 100;
        float FireRateMax = 150;

        info.name = info.rarity + " Light Cannon";
        Rarity = info.rarity;
        StartRolling(info);
        name = info.name;
        PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = (int)(AmmoMin + Mathf.Abs(Roll()) * (AmmoMax - AmmoMin));
        ReloadTime = ReloadMin + Mathf.Abs(Roll()) * (ReloadMax - ReloadMin);

        Stats.SetDamage(DamageTypes.Standard, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1;
    }
}
