using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawn : MonoBehaviour {

    public GameObject item;
    private float sTime = 0;
    private GameObject spawn;

    void Update()
    {/*
        if (Time.time - sTime > 10 & spawn == null)
        {
            spawn = Instantiate(item);
            spawn.GetComponent<PickupItem>().roll = BaseRoller.RollItem(100);
        }*/


    }
    void Start()
    {
        spawn = Instantiate(item, transform.position, transform.rotation, null);
        spawn.GetComponent<PickupItem>().roll = BaseRoller.RollItem(100);
    }
}
