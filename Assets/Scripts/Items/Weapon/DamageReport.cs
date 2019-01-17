using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DamageReport
{
    public bool Crit;
    public Dictionary<DamageTypes, float> Incoming;
    public Dictionary<DamageTypes, float> Reduction;

    public DamageReport()
    {
        Incoming = WeaponInfo.CreateEmptyDictionary();
        Reduction = WeaponInfo.CreateEmptyDictionary();
    }

    public void SetIncoming(WeaponInfo info)
    {
        for (int i = 0; i < Incoming.Count; i++)
        {
            DamageTypes type = (DamageTypes)i;
            Incoming[type] = (Crit) ? info.Crit(type) : info.TypeDamage(type);
        }
    }

    //public void SetReduction()
    //{
    //    for (int i = 0; i < Reduction.Count; i++)
    //    {
    //        DamageTypes type = (DamageTypes)i;
    //    }
    //}

    public float TotalIncoming()
    {
        float total = 0;

        foreach (float value in Incoming.Values)
        {
            total += value;
        }

        return total;
    }

    public float TotalReduction()
    {
        float total = 0;

        foreach (float value in Reduction.Values)
        {
            total += value;
        }

        return total;
    }

}
