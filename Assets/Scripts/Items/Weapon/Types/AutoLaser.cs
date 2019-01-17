using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLaser : ProjectileWeapon
{

    public override void RollStats(RollInfo info)
    {

        float FireRateMin = 80;
        float FireRateMax = 100;

        info.name = info.rarity + " Autolaser";
        Rarity = info.rarity;
        StartRolling(info);
        name = info.name;
        PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = 0;
        ReloadTime = 0;

        Stats.SetDamage(DamageTypes.Standard, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamageMultiplier = 1;
    }
}
