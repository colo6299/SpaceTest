using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public class ResistanceInfo
{
    private static Random rng = new Random((int)(DateTime.Now.Ticks / 17));

    public float Armor;
    public float CritChance;
    public float CritResistance;


    //public DamageReport ReduceDamage(float damage)
    //{
    //    DamageReport info = new DamageReport();

    //    if (rng.NextDouble() <= CritChance)
    //    {
    //        info.Crit = true;
    //        info.Reduction = (Armor * CritResistance);
    //    }

    //    info.Reduction += Armor;

    //    info.Remaining = (damage - info.Reduction < 0) ? 0 : damage - info.Reduction;

    //    return info;

    //}

}
