using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeaponStats : MonoBehaviour {


    public float StandardDamage = 0;
    public float HeatDamage = 0;
    public float MaterDamage = 0;

    public float CritChance = 0;
    public float CritDamage = 1;

	// Use this for initialization
	void Start () {

        WeaponInfo info = new WeaponInfo();

        info.SetDamage(DamageTypes.Standard, StandardDamage);
        info.SetDamage(DamageTypes.Heat, HeatDamage);
        info.SetDamage(DamageTypes.Antimater, MaterDamage);

        info.CritChance = CritChance;
        info.CritDamage = CritDamage;

        GetComponent<WeaponBase>().Stats = info;
	}
}
