using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ArmorBase : Item
{

    public float KineticResistance;
    public float HeatResistance;

    private Dictionary<DamageTypes, float> Resistances = new Dictionary<DamageTypes, float>
    {
        { DamageTypes.Kinetic, 0 },
        { DamageTypes.Heat, 0 }
    };

    public virtual void Start()
    {
        Resistances[DamageTypes.Kinetic] = KineticResistance;
        Resistances[DamageTypes.Heat] = HeatResistance;
    }

}

