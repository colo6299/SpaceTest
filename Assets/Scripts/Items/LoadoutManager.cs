using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour {

    //I've resigned myself to leaving this confusing.
    public void SlotItem(GameObject item)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == item.tag)
            {
                child.gameObject.SetActive(false);
            }
        }
        item.SetActive(true);
    }




}
