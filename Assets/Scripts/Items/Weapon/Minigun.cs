using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : WeaponBase, IItem
{  
    public void RollStats(RollInfo info)
    {
        StartRolling(info);
        Ammunition = 0;
        ReloadTime = 0;
        PowerLevel = Mathf.RoundToInt(Roll());
        RateOfFire = Roll() * 20 + 500;
        Stats.Damage = PowerLevel * (Roll() * 5 + 40);
    }

}
