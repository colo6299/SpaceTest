using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLaser : ProjectileWeapon
{
    public float FireRateMin = 80;
    public float FireRateMax = 100;

    public override void RollStats(RollInfo info)
    {
        base.RollStats(info);

        info.name = info.rarity + " Autolaser";
        name = info.name;

        // this has been moved to item.cs
        //Rarity = info.rarity;
        //StartRolling(info);
        //PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = 0;
        ReloadTime = 0;

        Accuracy = Mathf.Abs(Roll());

        Stats.SetDamage(DamageTypes.Kinetic, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1;
    }
}
