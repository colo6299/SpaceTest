using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : WeaponBase
{  
    public void RollStats(RollInfo _Info)
    {
        StartRolling(_Info);
        Ammunition = 0;
        ReloadTime = 0;
        Power = Mathf.RoundToInt(Roll());
        RateOfFire = Roll() * 20 + 500;
        Stats.Damage = Power * (Roll() * 5 + 40);
    }

}
