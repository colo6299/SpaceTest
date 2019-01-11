using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public RollInfo roll;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>().gameObject.tag == "Player")
        {
            //whaaaaaaat no way this is bunk... not at aaaaalllll
            BaseRoller.InstantiateItem(roll, other.GetComponentInParent<Rigidbody>().transform.position, other.GetComponentInParent<Rigidbody>().transform.rotation, other.GetComponentInParent<Rigidbody>().transform.GetChild(2)).SetActive(false);
            Destroy(gameObject);
        }
    }



}
