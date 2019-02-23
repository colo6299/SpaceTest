//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LoadoutManager : MonoBehaviour {

//    //I've resigned myself to leaving this confusing.
//    public void SlotItem(Item item)
//    {
//        foreach (Item child in transform.GetComponentsInChildren<Item>())
//        {
//            if (child.Type == item.Type)
//            {
//                child.gameObject.SetActive(false);
//            }
//        }

//        item.gameObject.SetActive(true);
//    }

//    public Item GetItemInSlot(SlotType slot)
//    {
//        foreach (Item child in transform.GetComponentsInChildren<Item>())
//        {
//            if (child.Type == slot)
//            {
//                return child;
//            }
//        }

//        return null;
//    }

//    public void TrashItem(Item item)
//    {
//        Destroy(item.gameObject);
//    }
//}
