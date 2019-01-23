using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HeatLance : BeamWeapon
{

    public float MinPowerConsumption = 25;
    public float MaxPowerConsumption = 200;

    public override void RollStats(RollInfo info)
    {
        base.RollStats(info);

        info.name = info.rarity + " Heat Lance";
        name = info.name;
        
        // this has been moved to item.cs
        //Rarity = info.rarity;
        //StartRolling(info);
        //PowerLevel = Mathf.RoundToInt(Roll());

        PowerConsumption = MinPowerConsumption + Mathf.Abs(Roll()) * (MaxPowerConsumption - MinPowerConsumption);

        TickRate = 1 + (int)(10 * Mathf.Abs(Roll()));

        Stats.SetDamage(DamageTypes.Heat, PowerLevel * 10 * Mathf.Abs(Roll()));
        Stats.CritChance = Mathf.Abs(Roll()) / 2;
        Stats.CritDamage = 1 + Mathf.Abs(Roll());
    }

}
