using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemsBar : MonoBehaviour {

    private GameObject primarySlot = null;
    private GameObject secondarySlot = null;
    private GameObject armorSlot = null;


    // Use this for initialization
	void Start ()
    {
        primarySlot = transform.Find("PrimarySlot").gameObject;
        secondarySlot = transform.Find("SecondarySlot").gameObject;
        armorSlot = transform.Find("ArmorSlot").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
