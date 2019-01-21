using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HeatLance : BeamWeapon
{

    public override void RollStats(RollInfo info)
    {
        float MinPowerConsumption = 25;
        float MaxPowerConsumption = 200;


        info.name = info.rarity + " Heat Lance";
        name = info.name;

        Rarity = info.rarity;
        StartRolling(info);
        PowerLevel = Mathf.RoundToInt(Roll());

        PowerConsumption = MinPowerConsumption + Mathf.Abs(Roll()) * (MaxPowerConsumption - MinPowerConsumption);

        TickRate = 1 + (int)(10 * Mathf.Abs(Roll()));

        Stats.SetDamage(DamageTypes.Heat, PowerLevel * 10 * Mathf.Abs(Roll()));
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1 + Mathf.Abs(Roll());
    }

}
