using System;
using System.Collections.Generic;
using System.Linq;

public enum DamageTypes { Standard, Heat, Antimater }

public class WeaponInfo
{
    private Dictionary<DamageTypes, float> DamageByTypes;

    public float CritChance;
    public float CritDamageMultiplier;


    public WeaponInfo()
    {
        DamageByTypes = CreateEmptyDictionary();
    }

    //public DamageTypes[] GetTypes()
    //{
    //    return DamageByTypes.Keys.ToArray();
    //}


    private float Crit(float dmg)
    {
        return dmg + (dmg * CritDamageMultiplier);
    }

    public float Crit()
    {
        return Crit(TotalDamage());
    }

    public float Crit(DamageTypes type)
    {
        return Crit(TypeDamage(type));
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

    public float TypeDamage(DamageTypes type)
    {
        return DamageByTypes[type];

        //if (DamageByTypes.ContainsKey(type))
        //{
        //    return DamageByTypes[type];
        //}

        //return 0;
    }

    public void SetDamage(DamageTypes type, float damage)
    {
        DamageByTypes[type] = damage;

        //if (!DamageByTypes.ContainsKey(type))
        //{
        //    DamageByTypes.Add(type, damage);
        //}
        //else
        //{
        //    DamageByTypes[type] = damage;
        //}

        if (DamageByTypes[type] < 0)
        {
            DamageByTypes[type] = 0;
        }
    }

    public void AddDamage(DamageTypes type, float amount)
    {
        //if (!DamageByTypes.ContainsKey(type))
        //{
        //    DamageByTypes.Add(type, amount);
        //}
        //else
        //{
        //    DamageByTypes[type] += amount;
        //}

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
