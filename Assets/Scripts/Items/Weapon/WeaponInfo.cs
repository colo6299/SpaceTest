using System;
using System.Collections.Generic;
using System.Linq;

public enum DamageTypes { Standard, Heat, Antimater }

public class WeaponInfo
{
    private Dictionary<DamageTypes, float> DamageByTypes;

    public float CritChance;
    public float CritDamage;


    public WeaponInfo()
    {
        DamageByTypes = CreateEmptyDictionary();
    }


    private float Crit(float dmg)
    {
        return dmg + (dmg * CritDamage);
    }

    public float Crit()
    {
        return Crit(TotalDamage());
    }

    public float Crit(DamageTypes type)
    {
        return Crit(Damage(type));
    }

    public float TotalDamage()
    {
        float total = 0;

        foreach (float value in DamageByTypes.Values)
        {
            total += value;
        }

        return total;
    }

    public float Damage(DamageTypes type)
    {
        return DamageByTypes[type];
    }

    public void SetDamage(DamageTypes type, float damage)
    {
        DamageByTypes[type] = damage;

        if (DamageByTypes[type] < 0)
        {
            DamageByTypes[type] = 0;
        }
    }

    public void AddDamage(DamageTypes type, float amount)
    {
        DamageByTypes[type] += amount;

        if (DamageByTypes[type] < 0)
        {
            DamageByTypes[type] = 0;
        }
    }

    public void RemoveDamage(DamageTypes type, float amount)
    {
        AddDamage(type, -amount);
    }

    /// <summary>
    /// the most glorious of crappy sorting algorithms
    /// </summary>
    /// <returns></returns>
    public DamageTypes[] Sort()
    {
        List<DamageTypes> types = new List<DamageTypes>();

        for (int i = 0; i < DamageByTypes.Count; i++)
        {
            DamageTypes type = DamageTypes.Standard;
            float best = -1;

            for (int j = 0; j < DamageByTypes.Count; j++)
            {
                DamageTypes t = (DamageTypes)j;

                if (DamageByTypes[t] > best && !types.Contains(t))
                {
                    best = DamageByTypes[t];
                    type = t;
                }
            }

            types.Add(type);
        }

        return types.ToArray();
    }

    public static Dictionary<DamageTypes, float> CreateEmptyDictionary()
    {
        Dictionary<DamageTypes, float> result = new Dictionary<DamageTypes, float>();

        DamageTypes[] types = (DamageTypes[])Enum.GetValues(typeof(DamageTypes));

        foreach (DamageTypes t in types)
        {
            result.Add(t, 0);
        }

        return result;
    }
}
