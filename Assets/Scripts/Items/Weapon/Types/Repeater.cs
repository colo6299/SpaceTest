using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : ProjectileWeapon
{

    public float FireRateMin = 600;
    public float FireRateMax = 800;

    public override void RollStats(RollInfo info)
    {
        base.RollStats(info);

        info.name = info.rarity + " Repeater";
        name = info.name;

        // this has been moved to Item.cs
        //Rarity = info.rarity;
        //StartRolling(info);
        //PowerLevel = Mathf.RoundToInt(Roll() * RarityMultiplyer());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = 0;
        ReloadTime = 0;
        Accuracy = Mathf.Abs(Roll());

        Stats.SetDamage(DamageTypes.Kinetic, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1;
    }

}
