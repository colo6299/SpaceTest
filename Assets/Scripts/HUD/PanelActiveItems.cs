using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActiveItems : MonoBehaviour {

    private GameObject primarySlot;
    private GameObject secondarySlot;
    private GameObject armorSlot;

    private Transform loadout;

    // Use this for initialization
	void Start ()
    {
        primarySlot = transform.Find("PrimarySlot").gameObject;
        secondarySlot = transform.Find("SecondarySlot").gameObject;
        armorSlot = transform.Find("ArmorSlot").gameObject;

        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        Transform loadout = obj.transform.Find("Loadout");
	}

}
