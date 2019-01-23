using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : ProjectileWeapon
{
    public int AmmoMin = 25;
    public int AmmoMax = 125;

    public float ReloadMin = 1;
    public float ReloadMax = 5;

    public float FireRateMin = 600;
    public float FireRateMax = 1000;

    public override void RollStats(RollInfo info)
    {
        base.RollStats(info);

        info.name = info.rarity + " Minigun";
        name = info.name;

        // this has been moved to item.cs
        //Rarity = info.rarity;
        //StartRolling(info);
        //PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = (int)(AmmoMin + Mathf.Abs(Roll()) * (AmmoMax - AmmoMin));
        ReloadTime = ReloadMin + Mathf.Abs(Roll()) * (ReloadMax - ReloadMin);
        Accuracy = Mathf.Abs(Roll());

        Stats.SetDamage(DamageTypes.Standard, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1;
    }

}
