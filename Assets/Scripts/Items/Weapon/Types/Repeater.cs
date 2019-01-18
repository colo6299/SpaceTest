using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : ProjectileWeapon
{
    public override void RollStats(RollInfo info)
    {

        float FireRateMin = 600;
        float FireRateMax = 800;

        info.name = info.rarity + " Repeater";
        Rarity = info.rarity;
        StartRolling(info);
        name = info.name;
        PowerLevel = Mathf.RoundToInt(Roll());

        RateOfFire = FireRateMin + Mathf.Abs(Roll()) * (FireRateMax - FireRateMin);
        Ammunition = 0;
        ReloadTime = 0;

        Stats.SetDamage(DamageTypes.Standard, PowerLevel + (PowerLevel / 4) * Roll());
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1;
    }

}
