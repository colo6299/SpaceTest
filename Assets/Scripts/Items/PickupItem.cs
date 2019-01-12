using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public RollInfo roll;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //somewhat better now
            BaseRoller.InstantiateItem(roll, other.transform.position, other.transform.rotation, other.transform.GetChild(0)).SetActive(false);
            Destroy(gameObject);
        }
    }



}
